import { Component, AfterViewInit, OnDestroy, ViewChild, OnInit } from '@angular/core';
import {MatDialog, MatSnackBar, MatTableDataSource, MatPaginator, PageEvent } from '@angular/material';
import * as _ from 'lodash';
import { User, Role } from '../../../models/user';
import { UserService } from '../../../services/user.service';
import { EditUserComponent } from '../edit-user/edit-user.component';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { MatTableState } from 'src/app/helpers/mattable.state';
import { UserStateService } from 'src/app/services/userState.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-userslist',
    templateUrl: './userslist.component.html',
    styleUrls: ['./userslist.component.css']
})

export class UsersListComponent implements OnInit, OnDestroy {
    
    currentUser: User;
    users: User[] = [];
    usersBackup: User[] = [];
    userSubscription: Subscription;
    

    length: number = 0;
    pageSize: number = 3;
    pageSizeOptions: number[] = [3, 6, 9, 12];

    dataSources: MatTableDataSource<User> = new MatTableDataSource();
    dataSource : User[] = [];
    filter: string;
    state: MatTableState;

    @ViewChild(MatPaginator, { static: false}) paginator: MatPaginator;

    constructor(private auth: AuthenticationService,
                private userService: UserService,
                private stateService: UserStateService,
                public dialog: MatDialog,
                public snackBar: MatSnackBar
        ) {
            this.currentUser = this.auth.currentUser;
            this.state = this.stateService.UserListState;
            }

     ngOnInit() {
        this.userService.getAll().subscribe(users => {
            this.users = users;
            this.usersBackup = _.cloneDeep(users);
            this.dataSources.data = this.users.slice(0,3);
            this.length = this.users.length;
        });
        this.refresh();
     }

    refresh() {
        this.userService.getRefrechAllUsers();
    }

    onPageChange(event: PageEvent){
        let startIndex = event.pageIndex * event.pageSize;
        let endIndex = startIndex + event.pageSize;
        if(endIndex > this.length){
            endIndex = this.length;
        }
        this.dataSources.data = this.users.slice(startIndex, endIndex);
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
        this.dataSources.data = this.users.slice(0,3);
    }
}