import { Component, OnInit, ViewChild, AfterViewInit, ElementRef, OnDestroy } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog, MAT_DIALOG_DATA, MatListItem, MatSnackBar, PageEvent, MatSortHeader } from '@angular/material';
import * as _ from 'lodash';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';

@Component({
    selector: 'app-userCard',
    templateUrl: './postlist.component.html',
    styleUrls: ['./postlist.component.css']
})

export class PostListComponent implements AfterViewInit {
    
    posts: Post[] = [];
    questions: Post[] = [];

    constructor(private postService: PostService,) {}

    ngAfterViewInit(): void {
        this.refresh();
    }

    refresh() {
        this.postService.getAllPosts().subscribe(posts => {
            // assigne les données récupérées aux posts
            this.posts = posts;

            this.posts.forEach(post => {
                if(post.title !== null){
                    this.questions.push(post);
                }
            });
        });

        
    }
}