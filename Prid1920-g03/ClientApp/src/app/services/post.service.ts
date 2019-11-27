import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User, IFriend } from '../models/user';
import { map, flatMap, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Post } from '../models/post';

@Injectable({ providedIn: 'root' })

export class PostService {
    
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAllPosts() {
    return this.http.get<Post[]>(`${this.baseUrl}api/post`).pipe(
      map(res => res.map(m => new Post(m)))
    );
  }

  getPostById(id: number) {
    return this.http.get<Post>(`${this.baseUrl}api/post/${id}`).pipe(
      map(m => !m ? null : new Post(m)),
      catchError(err => of(null))
    );
  }



//   public update(m: User): Observable<boolean> {
//     return this.http.put<User>(`${this.baseUrl}api/user/${m.id}`, m).pipe(
//       map(res => true),
//       catchError(err => {
//         console.error(err);
//         return of(false);
//       })
//     );
//   }

//   public delete(m: User): Observable<boolean> {
//     return this.http.delete<boolean>(`${this.baseUrl}api/user/${m.id}`).pipe(
//       map(res => true),
//       catchError(err => {
//         console.error(err);
//         return of(false);
//       })
//     );
//   }

//   public add(m: User): Observable<boolean> {
//     return this.http.post<User>(`${this.baseUrl}api/user`, m).pipe(
//       map(res => true),
//       catchError(err => {
//         console.error(err);
//         return of(false);
//       })
//     );
//   }

}