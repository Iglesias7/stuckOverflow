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
import { SinglePostListComponent } from '../single-post/single-post.component';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
    selector: 'app-post-view',
    templateUrl: './post-view.component.html',
    styleUrls: ['./post-view.component.css'],
})

export class PostViewComponent {
    currentUser: User;

    @Input() user: any;
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
    @Input() post: Post;
    @Input() Author: User;
    @Input() response: Post;


    constructor(private auth: AuthenticationService, private sp: SinglePostListComponent, private commentService: CommentService, private voteService: VoteService,private postService: PostService, private route: ActivatedRoute,public dialog: MatDialog, public snackBar: MatSnackBar,private router: Router) {
        this.currentUser = this.auth.currentUser;
        console.log(this.currentUser)
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
                        this.sp.ngOnInit();
                    });
                }
            });

            if(!res){
                this.voteService.upDown(newVote).subscribe(p => {
                    this.sp.ngOnInit();
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
                        this.sp.refrech();
                    }
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
                        this.sp.refrech();
                    }
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
                this.sp.refrech();
            }
            
        });
    }

    public accept(acceptedAnswerId: any){
        const id = this.id;
        const authorId = this.user.id;
        const post = new Post({id, acceptedAnswerId, authorId});
       
        if(this.Author.id == this.currentUser.id){
            this.postService.accept(post).subscribe();
            
            this.sp.refrech();
        }

        
    }


    public update() {
        const post = this.post;
        const id = this.post.id;
        var isQuestion = false;
        if(post.title != null)
            isQuestion = true;
        // const body = this.post.body;
        const tags = this.post.tags;
        const dlg = this.dialog.open(EditPostComponent, { data: { post, tags, isNew: false, isQuestion } });
        dlg.beforeClose().subscribe(res => {
            if (res) {
                _.assign(post, res);
                this.postService.update(res, id).subscribe(res => {
                    if (!res) {
                        this.snackBar.open(`la modification a échoué.`, 'Dismiss', { duration: 4000 });
                    }else{
                        this.snackBar.open(`la modification a réussi.`, 'Dismiss', { duration: 4000 });
                        this.sp.refrech();
                    }
                  
                });
            }
        });
    }

    public delete() {
        const post = this.post;
        var snackBarRef;
        if(post.title != null)
             snackBarRef = this.snackBar.open(`Post '${post.title}' will be deleted`, 'Undo', { duration: 4000 });
        else
            snackBarRef = this.snackBar.open(`Your response will be deleted`, 'Undo', { duration: 4000 });

        snackBarRef.afterDismissed().subscribe(res => {
            if (!res.dismissedByAction){
                this.postService.delete(post).subscribe();
                if(post.title != null)
                    this.router.navigate(['/posts']);
            }
            this.sp.refrech();
        });
    }
}