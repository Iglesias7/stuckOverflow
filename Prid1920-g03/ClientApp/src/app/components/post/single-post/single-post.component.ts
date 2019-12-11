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
    body: string;
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

    responseBody = "";


    constructor(private postService: PostService, private route: ActivatedRoute, public snackBar: MatSnackBar) {
        this.currentUser = this.postService.currentUser;
        this.getElem();
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
            this.body = post.body;
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

        const newVote = new Vote({upDown, authorId, postId});

        this.postService.getPostById(+postId).subscribe(post => {
            var res = false;
            post.votes.forEach(vote => {
                
                if(vote.authorId == authorId && vote.postId == postId && vote.upDown == upDown){
                    res = true;
                    const snackBarRef = this.snackBar.open(`Vous etes sur le point d'annuler votre vote.`, 'Undo', { duration: 4000 });
                    snackBarRef.afterDismissed().subscribe(res => {
                        if (res.dismissedByAction)
                            this.getElem();
                        else{
                            this.postService.deleteVote(vote).subscribe(p => {
                                this.getElem();
                            });
                        }
                    });
                }
            });

            if(!res){
                this.postService.upDown(newVote).subscribe(p => {
                    this.getElem();
                });
                
            }
        });
    }

    public reply(){
        const body = this.responseBody;
        const parentId = this.id;
        const authorId = this.currentUser.id;
        const title = null;
        const tags = null;

        const post =  new Post({body, authorId, parentId, title, tags});
        const id = this.route.snapshot.params['id'];
        this.postService.reply(post).subscribe(post =>{
            this.postService.getPostById(+id).subscribe(post => {
                this.post = post;
                this.body = post.body;
                this.numResponse = post.numResponse;
                this.responses = post.responses;
            });
        });
        this.responseBody = "";
    }
}