import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User, IFriend } from '../models/user';
import { map, flatMap, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Post } from '../models/post';
import { Subject } from 'rxjs';
import { Vote } from '../models/vote';

@Injectable({ providedIn: 'root' })

export class PostService {
    

  posts: Post[] = [];
  postsSubject = new Subject<Post[]>();

  public currentUser: User;
    
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.getPosts();
    const data = JSON.parse(sessionStorage.getItem('currentUser'));
      this.currentUser = data ? new User(data) : null;
  }

  public emitPost(){
    this.postsSubject.next(this.posts);
  }

  public getPosts(){
    this.getAllPosts().subscribe(posts => {
      this.posts = posts;
      this.emitPost();
    }); 
  }

  public getAllPosts() {
    return this.http.get<Post[]>(`${this.baseUrl}api/post`).pipe(
      map(res => res.map(m => new Post(m)))
    );
  }

  public getPostById(id: number) {
    return this.http.get<Post>(`${this.baseUrl}api/post/${id}`).pipe(
      map(m => !m ? null : new Post(m)),
      catchError(err => of(null))
    );
  }

  public getPostsByTagName(name: string){
    return this.http.get<Post>(`${this.baseUrl}api/post/getbytagname/${name}`).pipe(
      map(m => !m ? null : new Post(m))
    );
  }

  public reply(p: Post): Observable<boolean> {
    return this.http.post<Post>(`${this.baseUrl}api/post`, p).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }

  public add(p: Post): Observable<boolean> {
    console.log(p);
    return this.http.post<Post>(`${this.baseUrl}api/post`, p).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }

  public update(p: Post, id:number): Observable<boolean> {
    console.log(p)
    return this.http.put<Post>(`${this.baseUrl}api/post/${id}`, p).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }

  public delete(p: Post): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}api/post/${p.id}`).pipe(
      map(res => true),
      catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }

  
}