import { Component, OnInit } from '@angular/core';
import { User, IFriend } from '../../models/user';
import { UserService } from '../../services/user.service';
import { AuthenticationService } from '../../services/authentication.service';
import * as _ from 'lodash';

declare type RelationshipType = 'none' | 'follower' | 'followee' | 'mutual' | 'self';

@Component({
    templateUrl: 'relationships.component.html',
    styleUrls: ['relationships.component.css']
})

export class RelationshipsComponent implements OnInit {
    users: IFriend[] = [];
    usersBackup: IFriend[] = [];
    current: User;

    constructor(private userService: UserService,
        private authService: AuthenticationService) {
        this.current = this.authService.currentUser;
    }

    ngOnInit() {
        this.refresh();
    }

    refresh() {
        this.userService.getMembersWithRelationship(this.authService.currentUser.pseudo).subscribe(users => {
            this.users = users;
            this.usersBackup = _.cloneDeep(users);
        });
    }

    getRelationShip(user: User): RelationshipType {
        const currentUser = this.authService.currentUser;
        const follower = user.followees.findIndex(m => m === currentUser.id) !== -1;
        const followee = user.followers.findIndex(m => m === currentUser.id) !== -1;
        const mutual = followee && follower;

        if (user.pseudo == currentUser.pseudo) {
            return 'self';
        } else if (mutual) {
            return 'mutual';
        } else if (followee) {
            return 'followee';
        } else if (follower) {
            return 'follower';
        } else {
            return 'none';
        }
    }

    follow(user: IFriend) {
        // immediate frontend refresh
        switch (user.relationship) {
            case 'none':
                user.relationship = 'followee';
                break;
            case 'follower':
                user.relationship = 'mutual';
                break;
        }
        // async backend refresh
        this.userService.follow(this.current.pseudo, user.pseudo).subscribe(_ => this.refresh());
    }

    drop(member: IFriend) {
        // immediate frontend refresh
        switch (member.relationship) {
            case 'mutual':
                member.relationship = 'follower';
                break;
            case 'followee':
                member.relationship = 'none';
                break;
        }
        // async backend refresh
        this.userService.unfollow(this.current.pseudo, member.pseudo).subscribe(_ => this.refresh());
    }

    filterChanged(filter: string) {
        const lFilter = filter.toLowerCase();
        this.users = _.filter(this.usersBackup, m => {
            const str = (m.pseudo + ' ' + m.firstName).toLowerCase();
            return str.includes(lFilter);
        });
    }
}