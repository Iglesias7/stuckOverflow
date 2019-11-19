import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';
import { map, flatMap, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAll() {
    return this.http.get<User[]>(`${this.baseUrl}api/user`).pipe(
      map(res => res.map(m => new User(m)))
    );
  }

  getById(id: number) {
    return this.http.get<User>(`${this.baseUrl}api/user/${id}`).pipe(
      map(m => !m ? null : new User(m)),
      catchError(err => of(null))
    );
  }

  

  public update(m: User): Observable<boolean> {
    return this.http.put<User>(`${this.baseUrl}api/user/${m.id}`, m).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }

  public delete(m: User): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}api/user/${m.id}`).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }

  public add(m: User): Observable<boolean> {
    return this.http.post<User>(`${this.baseUrl}api/user`, m).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }

  public uploadPicture(pseudo, file): Observable<string> {
    console.log(file);
    const formData = new FormData();
    formData.append('pseudo', pseudo);
    formData.append('picture', file);
    return this.http.post<string>(`${this.baseUrl}api/user/upload`, formData).pipe(
      catchError(err => {
        console.error(err);
        return of(null);
      })
    );
  }

  public confirmPicture(pseudo, path): Observable<string> {
    console.log(pseudo, path);
    return this.http.post<string>(`${this.baseUrl}api/user/confirm`, { pseudo: pseudo, picturePath: path }).pipe(
      catchError(err => {
        console.error(err);
        return of(null);
      })
    );
  }

  public cancelPicture(path): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}api/user/cancel`, { picturePath: path }).pipe(
      catchError(err => {
        console.error(err);
        return of(null);
      })
    );
  }
}