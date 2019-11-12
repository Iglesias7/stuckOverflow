import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.css']
  })

export class SignupComponent implements OnInit, AfterViewInit {

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
    error = '';

    @ViewChild('pseudo', { static: true }) pseudo: ElementRef;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        if (this.authenticationService.currentUser) {
            this.router.navigate(['/']);
        }
    }

    ngOnInit() {
        this.ctlPseudo = this.formBuilder.control('', [Validators.required, Validators.minLength(3), 
            Validators.maxLength(10), Validators.pattern("^[A-Za-z][A-Za-z0-9_]{2,9}$")]);
        this.ctlPassword = this.formBuilder.control('', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]);
        this.ctlConfirmPassword = this.formBuilder.control('', Validators.required);
        this.ctlFirstName = this.formBuilder.control('', [Validators.minLength(3), Validators.maxLength(10)]);
        this.ctlLastName =  this.formBuilder.control('', [Validators.minLength(3), Validators.maxLength(10)]);
        this.ctlEmail = this.formBuilder.control('', [Validators.required, Validators.email]);
        this.signupForm = this.formBuilder.group({
            pseudo: ['', Validators.required],
            password: ['', Validators.required]
        });

        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    ngAfterViewInit() {
       
        setTimeout(_ => this.pseudo && this.pseudo.nativeElement.focus());
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
        this.authenticationService.login(this.f.pseudo.value, this.f.password.value)
            .subscribe(
                // si login est ok, on navigue vers la page demandée
                data => {
                    this.router.navigate([this.returnUrl]);
                },
                // en cas d'erreurs, on reste sur la page et on les affiche
                error => {
                    console.log(error);
                    this.error = error.error.errors.pseudo || error.error.errors.Password;
                    this.loading = false;
                }
            );
    }
}