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
    id = this.route.snapshot.params['id'];
    @Input() post: Post;
    postParent: Post;
    
    constructor(private auth: AuthenticationService, 
                private sp: SinglePostListComponent, 
                private commentService: CommentService, 
                private voteService: VoteService,
                private postService: PostService, 
                private route: ActivatedRoute,
                public dialog: MatDialog,
                public snackBar: MatSnackBar,
                private router: Router
        ) {
            this.currentUser = this.auth.currentUser;
            this.postService.getPostById(this.id).subscribe(post => {
                this.postParent = post;
            })
        }

    public upDown(postId: number,  upDown: number){
        
        if(this.currentUser){
            const authorId = this.currentUser.id;
            const newVote = new Vote({upDown, authorId, postId});

            if(upDown === 1 && this.currentUser.reputation < 15){
                this.snackBar.open(`Vous devez avoir au moins une réputation de 15 pour voter positivement.`, 'Dismiss', { duration: 4000 });
            }else if(upDown === -1 && this.currentUser.reputation < 30){
                this.snackBar.open(`Vous devez avoir au moins une réputation de 30 pour voter négativement.`, 'Dismiss', { duration: 4000 });
            }else{
                this.postService.getPostById(+postId).subscribe(post => {
                    var res = false;
                    post.votes.forEach(vote => {
                        if(vote.authorId == authorId && vote.postId == postId && vote.upDown == upDown){
                            res = true;
                            const snackBarRef = this.snackBar.open(`Vous etes sur le point d'annuler votre vote.`, 'Undo', { duration: 4000 });
                            snackBarRef.afterDismissed().subscribe(res => {
                                if (!res.dismissedByAction){
                                    this.voteService.deleteVote(vote).subscribe(vote => {
                                        this.sp.refrech();
                                    });
                                }
                            });
                        }
                    });
        
                    if(!res){
                        this.voteService.upDown(newVote).subscribe(p => {
                            this.sp.refrech();
                        });
                    }
                });
            }
            
        }else{
            this.snackBar.open(`Vous devez etre connecté pour voter.`, 'Dismiss', { duration: 4000 });
        }
        
    }

    public addComment() {
        if(this.currentUser){
            const comment = new Comment();
            const dlg = this.dialog.open(EditCommentComponent, { data: { comment, isNew: true } });
            dlg.beforeClose().subscribe(res => {
                if (res) {
                    this.commentService.add(this.post.id, res).subscribe(res => {
                        if (!res) {
                            this.snackBar.open(`There was an error at the server. The question has not been created! Please try again.`, 'Dismiss', { duration: 4000 });
                        }else{
                            this.snackBar.open(`add comment successfully`, 'Dismiss', { duration: 4000 });
                            this.sp.refrech();
                        }
                    });
                }
            });
        }else{
            this.snackBar.open(`Vous devez etre connecté pour ajouter votre commentaire.`, 'Dismiss', { duration: 4000 });
        }
    }

    public editComment(comment: any) {
        if(this.currentUser && this.currentUser.roleAsString == 'Admin' || this.currentUser.id == comment.commentUser.id){
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
    }

    public deleteComment(comment: any) {
        if(this.currentUser && this.currentUser.roleAsString == 'Admin' || this.currentUser.id == comment.commentUser.id){
            const id = comment.id;
            const snackBarRef = this.snackBar.open(`your comment will be deleted`, 'Undo', { duration: 4000 });
            snackBarRef.afterDismissed().subscribe(res => {
                if (!res.dismissedByAction){
                    this.commentService.delete(id).subscribe(()=>{
                        this.sp.refrech();
                    });
                }
            });
        }
    }

    public accept(acceptedAnswerId: any){
        if(this.currentUser && this.postParent.authorId == this.currentUser.id){
            const id = this.post.id;
            const parentId = this.post.parentId;
            const post = new Post({id, acceptedAnswerId, parentId});
            if(this.postParent.user.id == this.currentUser.id){
                this.postService.accept(post).subscribe(post =>{
                    this.sp.refrech();
                });
            }
        }
    }

    public update() {
        const post = this.post;
        const id = this.post.id;
        var isQuestion = false;
        if(post.title != null)
            isQuestion = true;
        const tags = this.post.tags;
        const dlg = this.dialog.open(EditPostComponent, { data: { post, tags, isNew: false, isQuestion }, height: "500px" });
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
                this.postService.delete(post).subscribe(() => {
                    if(post.title != null)
                        this.router.navigate(['/posts']);
                    else
                        this.sp.refrech();
                });
            }else{
                this.sp.refrech();
            }
        });
    }
}