import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Inject } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { FormGroup, FormControl, AsyncValidatorFn } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import * as _ from 'lodash';
import { User, Role } from 'src/app/models/user';
import { AuthenticationService } from 'src/app/services/authentication.service';


@Component({
    selector: 'app-edit-user-mat',
    templateUrl: './edit-user.component.html',
    styleUrls: ['./edit-user.component.css']
})


export class EditUserComponent {
    public editForm: FormGroup;
    public ctlPseudo: FormControl;
    public ctlFirstName: FormControl;
    public ctlLastName: FormControl;
    public ctlEmail: FormControl;
    public ctlPassword: FormControl;
    public ctlBirthDate: FormControl;
    public ctlReputation: FormControl;
    public ctlRole: FormControl;
    public isNew: boolean;


    constructor(public dialogRef: MatDialogRef<EditUserComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { user: User; isNew: boolean; },
        private formBuilder: FormBuilder,
        private userService: UserService,
        private auth: AuthenticationService
    ) {
        this.ctlPseudo = this.formBuilder.control('', [Validators.required, Validators.minLength(3), Validators.maxLength(10), Validators.pattern("^[A-Za-z][A-Za-z0-9_]{2,9}$")], [this.pseudoUsed()]);
        this.ctlPassword = this.formBuilder.control('', data.isNew ? [Validators.required, Validators.minLength(3)] : []);
        this.ctlFirstName = this.formBuilder.control('', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]);
        this.ctlLastName = this.formBuilder.control('', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]);
        this.ctlEmail = this.formBuilder.control('', [Validators.required, Validators.email], [this.emailUsed()]);
        this.ctlBirthDate = this.formBuilder.control('', [this.validateBirthDate()]);
        if(!this.ctlBirthDate){
            this.ctlBirthDate = null;
        }
        this.ctlReputation = this.formBuilder.control('0', []);
        this.ctlRole = this.formBuilder.control(Role.Member, []);

        this.editForm = this.formBuilder.group({
            pseudo: this.ctlPseudo,
            password: this.ctlPassword,
            firstName: this.ctlFirstName,
            lastName: this.ctlLastName,
            email: this.ctlEmail,
            birthDate: this.ctlBirthDate,
            role: this.ctlRole
        });

        this.isNew = data.isNew;
        this.editForm.patchValue(data.user);
    }

    // Validateur bidon qui vérifie que la valeur est différente
    // forbiddenValue(val: string): any {
    //     return (ctl: FormControl) => {
    //         if (ctl.value === val) {
    //             return { forbiddenValue: { currentValue: ctl.value, forbiddenValue: val } };
    //         }
    //         return null;
    //     };
    // }

    validateBirthDate(): any {
        return (ctl: FormControl) => {
            const date = new Date(ctl.value);
            const diff = Date.now() - date.getTime();
            if (diff < 0)
                return { futureBorn: true } 
            var age = new Date(diff).getUTCFullYear() - 1970;
            if (age < 18) 
                return { tooYoung: true };
            return null;
        };
    }

    emailUsed(): AsyncValidatorFn {
        let timeout: NodeJS.Timer;
        return (ctl: FormControl) => {
            clearTimeout(timeout);
            const email = ctl.value;
            return new Promise(resolve =>{
                timeout = setTimeout(() => {
                    if(ctl.pristine){
                        resolve(null);
                    } else {
                        this.auth.getByEmail(email).subscribe(user => {
                            resolve(user && email !== this.auth.currentUser.email ? null : {emailUsed: true } );
                        });
                    }
                }, 300);
            });
        };
    }

    // Validateur asynchrone qui vérifie si le pseudo n'est pas déjà utilisé par un autre membre
    pseudoUsed(): AsyncValidatorFn {
        let timeout: NodeJS.Timer;
        return (ctl: FormControl) => {
            clearTimeout(timeout);
            const pseudo = ctl.value;
            return new Promise(resolve => {
                timeout = setTimeout(() => {
                    if (ctl.pristine) {
                        resolve(null);
                    } else{
                        this.auth.getByPseudo(pseudo).subscribe(user => {
                            resolve(!(user) ? { pseudoUsed: true } : null);
                        });
                    }
                }, 300);
            });
        };
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    update() {
        this.dialogRef.close(this.editForm.value);
    }

    cancel() {
        this.dialogRef.close();
    }
}