import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, OnDestroy } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog, MAT_DIALOG_DATA, MatDialogRef, MatSnackBar, PageEvent, MatSortHeader } from '@angular/material';
import * as _ from 'lodash';
import { User } from '../../../models/user';
import { UserService } from '../../../services/user.service';

import { StateService } from 'src/app/services/state.service';
import { MatTableState } from 'src/app/helpers/mattable.state';
import { EditUserComponent } from '../edit-user/edit-user.component';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
    selector: 'app-userCard',
    templateUrl: './usersCard.component.html',
    styleUrls: ['./usersCard.component.css']
})

export class UserCardComponent implements AfterViewInit, OnDestroy {
    currentUser: User;
    users: User[] = [];
    usersBackup: User[] = [];

    filter: string;


    constructor(private userService: UserService,
                private stateService: StateService,
                private authServise: AuthenticationService,
                public dialog: MatDialog,
                public snackBar: MatSnackBar
        ) {
            this.currentUser = this.authServise.currentUser;
            }

    ngAfterViewInit(): void {
        // lie le datasource au sorter et au paginator

        // définit le predicat qui doit être utilisé pour filtrer les membres
        

        // établit les liens entre le data source et l'état de telle sorte que chaque fois que 
        // le tri ou la pagination est modifié l'état soit automatiquement mis à jour

        // récupère les données 
        this.refresh();
    }

    refresh() {
        this.userService.getAll().subscribe(users => {
            // assigne les données récupérées au datasource
            this.users = users;
            this.usersBackup = _.cloneDeep(users);
            // restaure l'état du filtre à partir du state
        });
    }

    
    // appelée quand on clique sur le bouton "edit" d'un membre
    edit(user: User) {
        
        const dlg = this.dialog.open(EditUserComponent, { data: { user, isNew: false } });
        dlg.beforeClose().subscribe(res => {
            if (res) {
                _.assign(user, res);
                this.userService.update(res, user.id).subscribe(res => {
                    if (!res) {
                        this.snackBar.open(`There was an error at the server. The update has not been done! Please try again.`, 'Dismiss', { duration: 4000 });
                        this.refresh();
                    }
                    this.refresh();
                });
            }
        });
    }

    // appelée quand on clique sur le bouton "delete" d'un membre
    delete(user: User) {
        const snackBarRef = this.snackBar.open(`User '${user.pseudo}' will be deleted`, 'Undo', { duration: 4000 });
        snackBarRef.afterDismissed().subscribe(res => {
            if (!res.dismissedByAction)
                this.userService.delete(user).subscribe(t => {
                    this.refresh();
                });
            
        });
    }

    // appelée quand on clique sur le bouton "new member"
    create() {
        const user = new User({});
        const dlg = this.dialog.open(EditUserComponent, { data: { user, isNew: true } });
        dlg.beforeClose().subscribe(res => {
            if (res) {
                this.userService.add(res).subscribe(res => {
                    if (!res) {
                        this.snackBar.open(`There was an error at the server. The member has not been created! Please try again.`, 'Dismiss', { duration: 10000 });
                    }
                    this.refresh();
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