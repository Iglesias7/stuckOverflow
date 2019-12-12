import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Post } from '../models/post';
import { PostService } from './post.service';

@Injectable({ providedIn: 'root' })

export class FilterService {
 
  constructor(private postService: PostService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  public getNewest() {
    this.http.get<Post[]>(`${this.baseUrl}api/filter/newest`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.postService.posts = posts;
      this.postService.emitPost();
    });
  }

  public getTagfilter() {
    this.http.get<Post[]>(`${this.baseUrl}api/filter/tagfilter`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.postService.posts = posts;
      this.postService.emitPost();
    });
  }

  public getUnanswered() {
    this.http.get<Post[]>(`${this.baseUrl}api/filter/unanswered`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.postService.posts = posts;
      this.postService.emitPost();
    });
  }

  public getHightVote() {
    this.http.get<Post[]>(`${this.baseUrl}api/filter/votefilter`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.postService.posts = posts;
      this.postService.emitPost();
    });
  }
}