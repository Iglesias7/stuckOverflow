import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User, IFriend } from '../models/user';
import { map, flatMap, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Post } from '../models/post';
import { Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })

export class PostService {

  posts: Post[] = [];
  postsSubject = new Subject<Post[]>();
    
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.getPosts();
  }

  emitPost(){
    this.postsSubject.next(this.posts);
  }

  getPosts(){
    this.getAllPosts().subscribe(posts => {
      this.posts = posts;
      this.emitPost();
    }); 
  }

  // getPostsNewest(){
  //   this.getNewest().subscribe(posts => {
  //     this.posts = posts;
  //     this.emitPost();
  //   }); 
  // }

  getAllPosts() {
    return this.http.get<Post[]>(`${this.baseUrl}api/post`).pipe(
      map(res => res.map(m => new Post(m)))
    );
  }

  getNewest() {
    this.http.get<Post[]>(`${this.baseUrl}api/post/newest`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.posts = posts;
      this.emitPost();
    });
  }

  getTagfilter() {
    this.http.get<Post[]>(`${this.baseUrl}api/post/tagfilter`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.posts = posts;
      this.emitPost();
    });
  }

  getUnanswered() {
    this.http.get<Post[]>(`${this.baseUrl}api/post/unanswered`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.posts = posts;
      this.emitPost();
    });
  }

  getHightVote() {
    this.http.get<Post[]>(`${this.baseUrl}api/post/votefilter`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.posts = posts;
      this.emitPost();
    });
  }

  getPostById(id: number) {
    return this.http.get<Post>(`${this.baseUrl}api/post/${id}`).pipe(
      map(m => !m ? null : new Post(m)),
      catchError(err => of(null))
    );
  }

  up(){
    this.http.get<Post[]>(`${this.baseUrl}api/post/tagfilter`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.posts = posts;
      this.emitPost();
    });
  }

  down(){
    this.http.get<Post[]>(`${this.baseUrl}api/post/tagfilter`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.posts = posts;
      this.emitPost();
    });
  }
}