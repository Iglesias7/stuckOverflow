import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatDialog, MatSnackBar, MatTableDataSource, PageEvent } from '@angular/material';
import * as _ from 'lodash';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { Subscription } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user.service';

@Component({
    selector: 'app-userCard',
    templateUrl: './single-post.component.html',
    styleUrls: ['./single-post.component.css'],
})

export class SinglePostListComponent implements OnInit, OnDestroy {
    currentUser: User;
    numComments: number;
    responseBody = "";
    user: User;

    id = this.route.snapshot.params['id'];

    post: Post;
    postSubsription: Subscription;

    responses: Post[];
    responsesSubsription: Subscription;

    constructor(private auth: AuthenticationService,
        private postService: PostService,
        private route: ActivatedRoute,
        private userService: UserService,
        public dialog: MatDialog,
        public snackBar: MatSnackBar
    ) {
        this.currentUser = this.auth.currentUser;
    }

    public ngOnInit() {
        this.postSubsription = this.postService.postSubject.subscribe(post => {
            this.post = post;
        });
        this.responsesSubsription = this.postService.responsesSubject.subscribe(responses => {
            this.responses = responses;
        });
        this.refrech();
    }

    public refrech() {
        this.postService.getRefrechPost(this.id);
        this.postService.emitAllResponses();
        if (this.currentUser)
            this.userService.getById(this.currentUser.id).subscribe(user => {
                this.user = user;
            })
    }

    public reply() {
        if (this.currentUser) {
            if (this.responseBody == "") {
                this.snackBar.open(`Oups! rien à ajouter!`, 'Dismiss', { duration: 4000 });
            } else {
                const body = this.responseBody;
                const parentId = this.post.id;
                const authorId = this.currentUser.id;
                const title = null;
                const tags = null;

                const post = new Post({ body, authorId, parentId, title, tags });
                this.postService.add(post).subscribe(post => {
                    this.refrech();
                });
            }

        } else {
            this.snackBar.open(`Vous devez etre connecté pour Répondre à une question.`, 'Dismiss', { duration: 4000 });
        }

        this.responseBody = "";
    }

    public ngOnDestroy(): void {
        this.postSubsription.unsubscribe();
        this.responsesSubsription.unsubscribe();
        this.snackBar.dismiss();
    }
}