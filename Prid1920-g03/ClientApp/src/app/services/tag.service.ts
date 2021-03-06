import { Injectable, Inject } from "@angular/core";
import { Tag } from "../models/tag";
import { HttpClient } from "@angular/common/http";
import { map, catchError } from "rxjs/operators";
import { User } from "../models/user";
import { Observable, of } from "rxjs";

@Injectable({ providedIn: 'root' })

export class TagService {
    tags: Tag[] = [];
    currentUser: User;

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){
        this.getTags();
        const data = JSON.parse(sessionStorage.getItem('currentUser'));
            this.currentUser = data ? new User(data) : null;    
    }

    getTags(){
        this.getAllTags().subscribe(tags => {
            this.tags = tags;
        })
    }


    getAllTags() {
        return this.http.get<Tag[]>(`${this.baseUrl}api/tag`).pipe(
            map(res => res.map(m => new Tag(m)))
        );
    }

    getByNbPosts() {
        return this.http.get<Tag[]>(`${this.baseUrl}api/tag/tagsbynbposts`).pipe(
            map(res => res.map(m => new Tag(m)))
        );
    }

    getByTimestamp() {
        return this.http.get<Tag[]>(`${this.baseUrl}api/tag/getbytimestamp`).pipe(
            map(res => res.map(m => new Tag(m)))
        );  
    }

    getByName() {
        return this.http.get<Tag[]>(`${this.baseUrl}api/tag/getbyname`).pipe(
            map(res => res.map(m => new Tag(m)))
        );
    }

    getTagByName(name: string): Observable<boolean> {
        return this.http.get<boolean>(`${this.baseUrl}api/tag/getTagByName/${name}`);
    }

    public update(tg: Tag, id: number): Observable<boolean>{
        return this.http.put<Tag>(`${this.baseUrl}api/tag/${id}`,tg).pipe(
            map(res => true),
            catchError(err => {
                console.error(err);
                return of(false);
            })
        );
    }

    public getRefreshAllTags(){
        this.getAllTags().subscribe(tags => {
            this.tags = tags;
        });
    }


    public delete(tg: Tag): Observable<boolean> {
        return this.http.delete<boolean>(`${this.baseUrl}api/tag/${tg.id}`).pipe(
          map(res => true),
         catchError(err => {
        console.error(err);
        return of(false);
      })
    );

     }
    
    public add(tg: Tag): Observable<boolean> {
        return this.http.post<Tag>(`${this.baseUrl}api/tag`, tg).pipe(
         map(res => true),
         catchError(err => {
        console.error(err);
        return of(false);
      })
    );
  }


}