import { Component, OnInit, AfterViewInit, ElementRef, OnDestroy } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog, MAT_DIALOG_DATA, MatListItem, MatSnackBar, PageEvent, MatSortHeader } from '@angular/material';
import * as _ from 'lodash';
import { FormBuilder, FormGroup, Validators, FormControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/user';
import { Vote } from 'src/app/models/vote';
import { post } from 'selenium-webdriver/http';

@Component({
    selector: 'app-userCard',
    templateUrl: './single-post.component.html',
    styleUrls: ['./single-post.component.css'],
})

export class SinglePostListComponent implements OnInit {
    currentUser: User;
    id: number;
    title: string;
    timestamp: string;
    authorId: number;
    parentId: number;
    acceptedAnswerId: number;
    user: any = {};
    numResponse: number;
    numVote: number;
    voteState: number;
    comments: (string | Comment)[];
    responses: (string | Post)[];
    tags: string[];
    votes: number;
    post: Post;

    numComments: number;

    responseBody = "My response";


    constructor(private postService: PostService, private route: ActivatedRoute) {
        this.currentUser = this.postService.currentUser;
    }

    ngOnInit() {
        this.getElem();
    }

    getElem(){
        const id = this.route.snapshot.params['id'];
        this.postService.getPostById(+id).subscribe(post => {
            this.post = post;
            this.id = post.id;
            this.title = post.title;
            this.authorId = post.authorId;
            this.timestamp = post.timestamp;
            this.tags = post.tags;
            this.comments = post.comments;
            this.numComments = post.numComments;
            this.user = post.user;
            this.numResponse = post.numResponse;
            this.responses = post.responses;
            this.voteState = post.voteState;
        });
    }

   

    upDown(postId: number,  upDown: number){
        
        const authorId = this.currentUser.id;

        const vote = new Vote({upDown, authorId, postId});
        
        const votes = [];
        votes.push(vote);
        const id = postId;
        const post = new Post({id, votes});
        this.postService.upDown(post).subscribe();
        this.getElem();
    }
}