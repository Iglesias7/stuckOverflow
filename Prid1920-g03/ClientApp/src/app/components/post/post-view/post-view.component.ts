import { Component, OnInit, Input} from '@angular/core';
import { MatDialog, MatSnackBar } from '@angular/material';
import * as _ from 'lodash';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { Vote } from 'src/app/models/vote';
import { EditPostComponent } from '../edit-post/edit-post.component';
import { EditCommentComponent } from '../../comment/edit-comment.component';
import { VoteService } from 'src/app/services/vote.service';
import { CommentService } from 'src/app/services/Comment.service';

@Component({
    selector: 'app-post-view',
    templateUrl: './post-view.component.html',
    styleUrls: ['./post-view.component.css'],
})

export class PostViewComponent {
    currentUser: User;

    @Input() postUser: any;
    @Input() comments: any[];
    @Input() isaccept: boolean;
    @Input() title: boolean;
    @Input() acceptedAnswerIdExist: boolean;
    @Input() tags: string[];
    @Input() numComments: number;
    @Input() voteState: string;
    @Input() body: string;
    @Input() timestamp: number;
    @Input() id: number;


    constructor(private commentService: CommentService, private voteService: VoteService,private postService: PostService, private route: ActivatedRoute,public dialog: MatDialog, public snackBar: MatSnackBar,private router: Router) {
        this.currentUser = this.postService.currentUser;
    }

    public upDown(postId: number,  upDown: number){
        
        const authorId = this.currentUser.id;

        const newVote = new Vote({upDown, authorId, postId});

        this.postService.getPostById(+postId).subscribe(post => {
            var res = false;
            post.votes.forEach(vote => {
                
                if(vote.authorId == authorId && vote.postId == postId && vote.upDown == upDown){
                    res = true;
                    const snackBarRef = this.snackBar.open(`Vous etes sur le point d'annuler votre vote.`, 'Undo', { duration: 4000 });
                    snackBarRef.afterDismissed().subscribe(res => {
                        if (!res.dismissedByAction){
                            this.voteService.deleteVote(vote).subscribe();
                        }
                        this.postService.getRefrechPost(this.id);
                    });
                }
            });

            if(!res){
                this.voteService.upDown(newVote).subscribe(p => {
                    this.postService.getRefrechPost(this.id);
                });
                
            }
        });
    }

    public addComment() {
        const comment = new Comment();
        const dlg = this.dialog.open(EditCommentComponent, { data: { comment, isNew: true } });
        dlg.beforeClose().subscribe(res => {
            if (res) {
                this.commentService.add(this.id, res).subscribe(res => {
                    if (!res) {
                        this.snackBar.open(`There was an error at the server. The question has not been created! Please try again.`, 'Dismiss', { duration: 4000 });
                    }else{
                        this.snackBar.open(`add comment successfully`, 'Dismiss', { duration: 4000 });
                    }
                    this.postService.getRefrechPost(this.id);
                });
            }
        });
    }

    public editComment(comment: any) {
        const id = comment.id;
        const dlg = this.dialog.open(EditCommentComponent, { data: { comment, isNew: false } });
        dlg.beforeClose().subscribe(res => {
            if (res) {
                this.commentService.update(res, id).subscribe(res => {
                    if (!res) {
                        this.snackBar.open(`There was an error at the server. The comment has not been update! Please try again.`, 'Dismiss', { duration: 4000 });
                    }else{
                        this.snackBar.open(`update comment successfully`, 'Dismiss', { duration: 4000 });
                    }
                    this.postService.getRefrechPost(this.id);
                });
            }
        });
    }

    public deleteComment(comment: any) {
        const id = comment.id;
        const snackBarRef = this.snackBar.open(`your comment will be deleted`, 'Undo', { duration: 4000 });
        snackBarRef.afterDismissed().subscribe(res => {
            if (!res.dismissedByAction){
                this.commentService.delete(id).subscribe();
            }
            this.postService.getRefrechPost(this.id);
        });
    }

    public accept(acceptedAnswerId: any){
        const id = this.id;
        const authorId = this.postUser.id;
        const post = new Post({id, acceptedAnswerId, authorId});
       
        // if(this.postUser.id == this.currentUser.id){
            this.postService.accept(post).subscribe();
            this.postService.getRefrechPost(id);
        // }
    }
}