import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, flatMap } from 'rxjs/operators';
import { User } from '../models/user';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    // l'utilisateur couramment connecté (undefined sinon)
    public currentUser: User;

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
      // au départ on récupère un éventuel utilisateur stocké dans le sessionStorage
      const data = JSON.parse(sessionStorage.getItem('currentUser'));
      this.currentUser = data ? new User(data) : null;
    }

    login(pseudo: string, password: string) {
      return this.http.post<User>(`${this.baseUrl}api/user/authenticate`, { pseudo, password })
        .pipe(map(user => {
          user = new User(user);
          // login successful if there's a jwt token in the response
          if (user && user.token) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            sessionStorage.setItem('currentUser', JSON.stringify(user));
            this.currentUser = user;
          }
          return user;
        }));
    }

    refresh() {
      return this.http.post<User>(`${this.baseUrl}api/user/refresh`, this.currentUser).pipe(
        map(res => {
          this.currentUser.token = res.token;
          this.currentUser.refreshToken = res.refreshToken;
          sessionStorage.setItem('currentUser', JSON.stringify(this.currentUser));
          return res;
        })
      );
    }

    public signup(pseudo: string, password: string, email: string,firstname: string, lastname: string, birthdate: string): Observable<User>{
      return this.http.post<User>(`${this.baseUrl}api/user/signup`, { pseudo: pseudo, password: password, email: email, firstName: firstname, lastName: lastname, reputation: 0, birthDate: birthdate}).pipe(
        flatMap(res => this.login(pseudo, password))
      );
    }

    getByPseudo(pseudo: string): Observable<boolean> {
      return this.http.get<boolean>(`${this.baseUrl}api/user/availablePseudo/${pseudo}`);
    }
  
    getByEmail(email: string): Observable<boolean> {
      return this.http.get<boolean>(`${this.baseUrl}api/user/availableEmail/${email}`);     
    }

    
    logout() {
      // remove user from local storage to log user out
      sessionStorage.removeItem('currentUser');
      this.currentUser = null;
    }
}