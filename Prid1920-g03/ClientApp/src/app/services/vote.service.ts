import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Post } from '../models/post';
import { Vote } from '../models/vote';

@Injectable({ providedIn: 'root' })

export class VoteService {
    
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  public upDown(v: Vote): Observable<boolean>{
    return this.http.post<Post>(`${this.baseUrl}api/vote`, v).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    ); 
  }

  public deleteVote(v: Vote): Observable<boolean>{
    return this.http.delete<boolean>(`${this.baseUrl}api/vote/${v.postId}`).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    ); 
  }
  
}