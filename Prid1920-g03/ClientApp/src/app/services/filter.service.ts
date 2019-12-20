import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Post } from '../models/post';
import { PostService } from './post.service';

@Injectable({ providedIn: 'root' })

export class FilterService {
 
  constructor(private postService: PostService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  public getNewest(filter: string = "") {
  this.http.get<Post[]>(`${this.baseUrl}api/filter/newest/${filter}`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.postService.posts = posts;
      this.postService.emitAllPosts();
    });
  }

  public getTagfilter(filter: string = "") {
    this.http.get<Post[]>(`${this.baseUrl}api/filter/tagfilter/${filter}`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.postService.posts = posts;
      this.postService.emitAllPosts();
    });
  }

  public getUnanswered(filter: string = "") {
    this.http.get<Post[]>(`${this.baseUrl}api/filter/unanswered/${filter}`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.postService.posts = posts;
      this.postService.emitAllPosts();
    });
  }

  public getall(filter: string = "") {
    this.http.get<Post[]>(`${this.baseUrl}api/filter/getall/${filter}`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.postService.posts = posts;
      this.postService.emitAllPosts();
    });
  }

  public getHightVote() {
    this.http.get<Post[]>(`${this.baseUrl}api/filter/votefilter`).pipe(
      map(res => res.map(m => new Post(m)))
    ).subscribe(posts => {
      this.postService.posts = posts;
      this.postService.emitAllPosts();
    });
  }
}