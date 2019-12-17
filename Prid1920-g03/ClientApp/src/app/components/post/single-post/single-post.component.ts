import { Component, OnInit} from '@angular/core';
import { MatDialog, MatSnackBar } from '@angular/material';
import * as _ from 'lodash';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { EditPostComponent } from '../edit-post/edit-post.component';
import { VoteService } from 'src/app/services/vote.service';
import { CommentService } from 'src/app/services/Comment.service';
import { Subscription } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
    selector: 'app-userCard',
    templateUrl: './single-post.component.html',
    styleUrls: ['./single-post.component.css'],
})

export class SinglePostListComponent implements OnInit {
    currentUser: User;
    numComments: number;
    responseBody = "";
    
    id = this.route.snapshot.params['id'];

    post: Post;
    postSubsription: Subscription;
    
    responses: Post[];
    responsesSubsription: Subscription;

    constructor(private auth: AuthenticationService, private commentService: CommentService, private voteService: VoteService,private postService: PostService, private route: ActivatedRoute,public dialog: MatDialog, public snackBar: MatSnackBar,private router: Router) {
        this.currentUser = this.auth.currentUser;
    }

    public ngOnInit() {
        this.refrech();
    }

    public refrech(){
        this.postSubsription = this.postService.postSubject.subscribe(post => {
            this.post = post;
        });
        this.responsesSubsription = this.postService.responsesSubject.subscribe(responses => {
            this.responses = responses;
        });

        this.postService.getRefrechPost(this.id);
        this.postService.emitAllResponses();
    }

    public reply(){
        const body = this.responseBody;
        const parentId = this.post.id;
        const authorId = this.currentUser.id;
        const title = null;
        const tags = null;

        const post =  new Post({body, authorId, parentId, title, tags});
        this.postService.add(post).subscribe(post =>{
            this.postService.getRefrechPost(this.id);
            this.postService.emitAllResponses();
        });
        this.responseBody = "";
    }
}