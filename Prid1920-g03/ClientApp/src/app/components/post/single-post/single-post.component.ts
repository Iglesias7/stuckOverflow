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

@Component({
    selector: 'app-userCard',
    templateUrl: './single-post.component.html',
    styleUrls: ['./single-post.component.css'],
})

export class SinglePostListComponent implements OnInit {
    currentUser: User;
    post: Post;
    postUser: any;
    numComments: number;
    responseBody = "";
    


    constructor(private commentService: CommentService, private voteService: VoteService,private postService: PostService, private route: ActivatedRoute,public dialog: MatDialog, public snackBar: MatSnackBar,private router: Router) {
        this.currentUser = this.postService.currentUser;
        this.getElem();
    }

    public ngOnInit() {
        this.getElem();
    }

    public getElem(){
        const id = this.route.snapshot.params['id'];
        this.postService.getPostById(+id).subscribe(post => {
            this.post = post;
            this.postUser = post.postUser;
            console.log(post);
        });
    }

    public reply(){
        const body = this.responseBody;
        const parentId = this.post.id;
        const authorId = this.currentUser.id;
        const title = null;
        const tags = null;

        const post =  new Post({body, authorId, parentId, title, tags});
        const id = this.route.snapshot.params['id'];
        this.postService.reply(post).subscribe(post =>{
            this.postService.getPostById(+id).subscribe(post => {
                this.post = post;
                this.post.body = post.body;
                this.post.numResponse = post.numResponse;
                this.post.responses = post.responses;
            });
        });
        this.responseBody = "";
    }

    public updateQuestion() {
        const post = this.post;
        const id = this.post.id;
        // const body = this.post.body;
        const tags = this.post.tags;
        const dlg = this.dialog.open(EditPostComponent, { data: { post, tags, isNew: false } });
        dlg.beforeClose().subscribe(res => {
            if (res) {
                _.assign(post, res);
                this.postService.update(res, id).subscribe(res => {
                    if (!res) {
                        this.snackBar.open(`Vous etes sur le point d'annuler votre vote.`, 'Dismiss', { duration: 4000 });
                        this.getElem();
                    }
                });
            }
        });
    }

    public deleteQuestion() {
        const post = this.post;
        const snackBarRef = this.snackBar.open(`Post '${post.title}' will be deleted`, 'Undo', { duration: 4000 });
        snackBarRef.afterDismissed().subscribe(res => {
            if (!res.dismissedByAction){
                this.postService.delete(post).subscribe();
                this.router.navigate(['/posts']);
            }
                
        });
    }

    
}