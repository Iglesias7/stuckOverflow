import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';


@Injectable({ providedIn: 'root' })

export class CommentService {
    
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  public add(c: Comment): Observable<boolean> {
    return this.http.post<Comment>(`${this.baseUrl}api/comment`, c).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }

}