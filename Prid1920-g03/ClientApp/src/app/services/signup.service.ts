import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
@Injectable({ providedIn: 'root' })

export class SignupService {

    public newUser : User;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl : string){

    }

    checkPseudoNotTaken(pseudo: string ){
        return this.http.post<User>(`${this.baseUrl}api/user/validatepseudonottaken`, {pseudo})
        .pipe(map(user => {
            user = new User(user);
            return user;
        }))
    }

    checkEmailNotTaken(email: string ){
        return this.http.post<User>(`${this.baseUrl}api/user/validatemailnottaken`, {email})
        .pipe(map(user => {
            user = new User(user);
            return user;
        }))
    }
}