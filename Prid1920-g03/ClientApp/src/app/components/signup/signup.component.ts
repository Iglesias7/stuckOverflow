import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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