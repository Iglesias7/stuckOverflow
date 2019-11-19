import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { AuthenticationService } from '../../../services/authentication.service';


@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.css']
  })

export class SignupComponent {

    public signupForm: FormGroup;
    public ctlPseudo: FormControl;
    public ctlPassword: FormControl;
    public ctlConfirmPassword: FormControl;
    public ctlFirstName: FormControl;
    public ctlLastName: FormControl;
    public ctlEmail: FormControl;
    public ctlBirthDate: FormControl;
    public loading = false;   

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private authenticationService: AuthenticationService,
    ) {
        this.ctlPseudo = this.formBuilder.control('', [Validators.required, Validators.minLength(3), Validators.maxLength(20), Validators.pattern("^[A-Za-z][A-Za-z0-9_]{2,9}$")], [this.pseudoUsed()]);
        this.ctlPassword = this.formBuilder.control('', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]);
        this.ctlConfirmPassword = this.formBuilder.control('', [Validators.required, Validators.minLength(3)]);
        this.ctlFirstName = this.formBuilder.control('', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]);
        this.ctlLastName =  this.formBuilder.control('', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]);
        this.ctlEmail = this.formBuilder.control('', [Validators.required, Validators.email, Validators.pattern("^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$")], [this.emailUsed()]);
        this.ctlBirthDate = this.formBuilder.control('', [this.validateBirthDate()]);

        this.signupForm = this.formBuilder.group({
            pseudo: this.ctlPseudo,
            password: this.ctlPassword,
            confirm_password: this.ctlConfirmPassword,
            email: this.ctlEmail,
            firstName: this.ctlFirstName,
            lastName: this.ctlLastName,
            birthDate: this.ctlBirthDate,
        }, { validator: this.validatePasswords}, {validator: this.validateFirstnameAndLastname});
    }

    validateBirthDate(): AsyncValidatorFn {
        let timeout: NodeJS.Timer;
        return (ctl: FormControl) => {
            clearTimeout(timeout);
            const date = new Date(ctl.value);
            const diff = Date.now() - date.getTime();
            var age = new Date(diff).getUTCFullYear() - 1970;
            return new Promise(resolve => {
                timeout = setTimeout(() => {
                    if (ctl.pristine) {
                        resolve(null);
                    } else if(diff < 0) {
                        resolve (!(diff < 0) ? null : { futureBorn: true } );
                    }else {
                        resolve (age >= 18 ? null : { tooYoung: true });
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
                    } else {
                        this.authenticationService.getByPseudo(pseudo).subscribe(user => {
                            resolve(user ? null : { pseudoUsed: true } );
                        });
                    }
                }, 300);
            });
        };
    }

    lastName(): AsyncValidatorFn {
        let timeout: NodeJS.Timer;
        return (ctl: FormControl) => {
            clearTimeout(timeout);
            const pseudo = ctl.value;
            return new Promise(resolve => {
                timeout = setTimeout(() => {
                    if (this.firstName === null) {
                        resolve( { lastnameRequired: true } );
                    }
                }, 300);
            });
        };
    }

    firstName(): AsyncValidatorFn {
        let timeout: NodeJS.Timer;
        return (ctl: FormControl) => {
            clearTimeout(timeout);
            const pseudo = ctl.value;
            return new Promise(resolve => {
                timeout = setTimeout(() => {
                    if (this.lastName === null) {
                        resolve( { firstnameRequired: true } );
                    }
                }, 300);
            });
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
                        this.authenticationService.getByEmail(email).subscribe(user => {
                            resolve(user ? null : {emailUsed: true } );
                        });
                    }
                }, 300);
            });
        };
    }

    validatePasswords(group: FormGroup) : ValidationErrors {
        if(!group.value) {return null;}
        return group.value.password === group.value.confirm_password ? null : { passwordNotConfirmed: true };
    }

    validateFirstnameAndLastname(group: FormGroup) : ValidationErrors {
        if(group.value.firstName !== "" && group.value.lastName === "")
            return true;
        else if(group.value.firstName === "" && group.value.lastName !== "")
            return true;
        else
            return null;
    }

    signup() {
        this.loading = true;
        this.authenticationService.signup(this.ctlPseudo.value, this.ctlPassword.value, this.ctlFirstName.value, this.ctlLastName.value, this.ctlEmail.value, this.ctlBirthDate.value).subscribe(() => {
            if (this.authenticationService.currentUser) {
                // Redirect the user
                this.router.navigate(['/']);
            }
        });
    }
}