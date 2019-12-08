import { Injectable, Inject } from "@angular/core";
import { Tag } from "../models/tag";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { User } from "../models/user";

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

}