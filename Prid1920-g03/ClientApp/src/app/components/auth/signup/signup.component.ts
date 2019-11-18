import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';
import { AuthenticationService } from '../../../services/authentication.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';


@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.css']
  })

export class SignupComponent implements OnInit {

    signupForm: FormGroup;
    loading = false;    
    submitted = false;  

    returnUrl: string;
    ctlPseudo: FormControl;
    ctlPassword: FormControl;
    ctlConfirmPassword: FormControl;
    ctlFirstName: FormControl;
    ctlLastName: FormControl;
    ctlEmail: FormControl;
    ctlBirthDate: FormControl;

    @ViewChild('pseudo', { static: true }) pseudo: ElementRef;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private userService: UserService
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.currentUser) {
            this.router.navigate(['/counter']);
        }
    }

    ngOnInit() {
        this.ctlPseudo = this.formBuilder.control('', [Validators.required, Validators.minLength(3), 
            Validators.maxLength(10), Validators.pattern("^[A-Za-z][A-Za-z0-9_]{2,9}$"), this.pseudoUsed()]);
        this.ctlPassword = this.formBuilder.control('', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]);
        this.ctlConfirmPassword = this.formBuilder.control('', [Validators.required, this.validatePasswords()]);
        this.ctlFirstName = this.formBuilder.control('', [Validators.minLength(3), Validators.maxLength(10)]);
        this.ctlLastName =  this.formBuilder.control('', [Validators.minLength(3), Validators.maxLength(10)]);
        this.ctlEmail = this.formBuilder.control('', [Validators.required, Validators.email, this.emailUsed()]);


        this.signupForm = this.formBuilder.group({
            pseudo: this.ctlPseudo,
            password: this.ctlPassword,
            confirm_password: this.ctlConfirmPassword,
            email: this.ctlEmail,
            firstName: this.ctlFirstName,
            lastName: this.ctlLastName,
            birthDate: this.ctlBirthDate,
        });
        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/counter';
    }
   

    // On définit ici un getter qui permet de simplifier les accès aux champs du formulaire dans le HTML
    get f() { return this.signupForm.controls; }
    
    /**
     * Cette méthode est bindée sur l'événement onsubmit du formulaire. On va y faire le
     * login en faisant appel à AuthenticationService.
     */
    onSubmit() {
        this.submitted = true;
        // on s'arrête si le formulaire n'est pas valide
        if (this.signupForm.invalid) return;
        this.loading = true;

        this.create();
    }

   
    create() {
        
        const user = new User({});
        user.pseudo = this.f.pseudo.value;
        user.email = this.f.email.value;
        user.firstName = this.f.firstName.value;
        user.lastName = this.f.lastName.value;
        user.password = this.f.password.value;
        user.birthDate = this.f.birthDate.value;
        
        this.userService.add(user).subscribe(
            data => {
                this.authenticationService.login(this.f.pseudo.value, this.f.password.value)
                .subscribe(
                    // si login est ok, on navigue vers la page demandée
                    data => {
                        this.router.navigate([this.returnUrl]);
                    },
                    // en cas d'erreurs, on reste sur la page et on les affiche
                    error => {
                        const errors = error.error.errors;
                        for (let field in errors) {
                            this.signupForm.get(field.toLowerCase()).setErrors({ custom: errors[field] })
                        }
                        this.loading = false;
                    }
                );
            },
            // en cas d'erreurs, on reste sur la page et on les affiche
            error => {
                const errors = error.error.errors;
                for (let field in errors) {
                    this.signupForm.get(field.toLowerCase()).setErrors({ custom: errors[field] })
                }
                this.loading = false;
            }
        );            
    }

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

    // Validateur asynchrone qui vérifie si le pseudo n'est pas déjà utilisé par un autre membre
    pseudoUsed(): any {
        let timeout: NodeJS.Timer;
        return (ctl: FormControl) => {
            clearTimeout(timeout);
            const pseudo = ctl.value;
            return new Promise(resolve => {
                timeout = setTimeout(() => {
                    if (ctl.pristine) {
                        resolve(null);
                    } else {
                        this.userService.getByPseudo(pseudo).subscribe(member => {
                            resolve(member ? { pseudoUsed: true } : null);
                        });
                    }
                }, 300);
            });
        };
    }


    emailUsed(): any {
        let timeout: NodeJS.Timer;
        return (ctl: FormControl) => {
            clearTimeout(timeout);
            const email = ctl.value;
            return new Promise(resolve =>{
                timeout = setTimeout(() => {
                    if(ctl.pristine){
                        resolve(null);
                    } else {
                        this.userService.getByEmail(email).subscribe(member => {
                            resolve(member ? {emailUsed: true } : null);
                        });
                    }
                }, 300);
            });
        };
    }

   validatePasswords() : any {
        let notNull = false;
        let pw = this.ctlPassword.value;
        let cfpw = this.ctlConfirmPassword.value;       
        if((pw != "") && (cfpw != ""))
            notNull = true;
        if(notNull)
            return (pw == cfpw);
        else
            return null;
   }

}