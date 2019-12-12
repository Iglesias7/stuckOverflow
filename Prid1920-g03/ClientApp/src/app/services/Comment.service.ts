import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';


@Injectable({ providedIn: 'root' })

export class CommentService {
    
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  public add(postId: number,c: Comment): Observable<boolean> {
    return this.http.post<Comment>(`${this.baseUrl}api/comment/${postId}`, c).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }

  public update(c: Comment, id:number): Observable<boolean> {
    return this.http.put<Comment>(`${this.baseUrl}api/comment/${id}`, c).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }

  public delete(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}api/comment/${id}`).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }

}