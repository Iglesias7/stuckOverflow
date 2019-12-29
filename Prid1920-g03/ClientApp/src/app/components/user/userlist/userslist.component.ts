import { Component, AfterViewInit, OnDestroy } from '@angular/core';
import {MatDialog, MatSnackBar } from '@angular/material';
import * as _ from 'lodash';
import { User, Role } from '../../../models/user';
import { UserService } from '../../../services/user.service';
import { StateService } from 'src/app/services/state.service';
import { EditUserComponent } from '../edit-user/edit-user.component';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
    selector: 'app-userslist',
    templateUrl: './userslist.component.html',
    styleUrls: ['./userslist.component.css']
})

export class UsersListComponent implements AfterViewInit, OnDestroy {
    currentUser: User;
    users: User[] = [];
    usersBackup: User[] = [];
    filter: string;

    constructor(private userService: UserService,
                private auth: AuthenticationService,
                private stateService: StateService,
                public dialog: MatDialog,
                public snackBar: MatSnackBar
        ) {
            this.currentUser = this.auth.currentUser;
            }

    ngAfterViewInit(): void {
        this.refresh();
    }

    refresh() {
        this.userService.getAll().subscribe(users => {
            this.users = users;
            this.usersBackup = _.cloneDeep(users);
        });
    }
    
    edit(user: User) {
        const dlg = this.dialog.open(EditUserComponent, { data: { user, isNew: false } });
        const id = user.id;
        dlg.beforeClose().subscribe(res => {
            if (res) {
                _.assign(user, res);
                this.userService.update(res, id).subscribe(res => {
                    if (!res) {
                        this.snackBar.open(`There was an error at the server. The update has not been done! Please try again.`, 'Dismiss', { duration: 10000 });
                        this.refresh();
                    }
                });
            }
        });
    }

    delete(user: User) {
        const snackBarRef = this.snackBar.open(`User '${user.pseudo}' will be deleted`, 'Undo', { duration: 10000 });
        snackBarRef.afterDismissed().subscribe(res => {
            if (!res.dismissedByAction)
                this.userService.delete(user).subscribe(()=>{
                    this.refresh();
                });
        });
    }

    create() {
        const user = new User({});
        const dlg = this.dialog.open(EditUserComponent, { data: { user, isNew: true } });
        dlg.beforeClose().subscribe(res => {
            if (res) {
                this.userService.add(res).subscribe(res => {
                    if (!res) {
                        this.snackBar.open(`There was an error at the server. The member has not been created! Please try again.`, 'Dismiss', { duration: 10000 });
                        this.refresh();
                    }else{
                        this.refresh();
                    }
                });
            }
        });
    }

    ngOnDestroy(): void {
        this.snackBar.dismiss();
    }

    filterChanged(filter: string) {
        const lFilter = filter.toLowerCase();
        this.users = _.filter(this.usersBackup, m => {
            const str = (m.pseudo + ' ' + m.firstName).toLowerCase();
            return str.includes(lFilter);
        });
    }
}