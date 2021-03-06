import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { MatDialog, MatSnackBar} from '@angular/material';
import * as _ from 'lodash';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';
import { Subscription } from 'rxjs';
import { EditPostComponent } from '../edit-post/edit-post.component';
import { FilterService } from 'src/app/services/filter.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { User } from 'src/app/models/user';
import { StateService } from 'src/app/services/state.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-userCard',
    templateUrl: './postlist.component.html',
    styleUrls: ['./postlist.component.css'],
})

export class PostListComponent implements OnInit, OnDestroy {
    currentUser: User;
    posts: Post[] = [];
    postsBackup: Post[] = [];
    postsSubsription: Subscription;
    filter: string = this.route.snapshot.params['name'];

    constructor(private auth: AuthenticationService, 
                private filterService: FilterService,
                private postService: PostService, 
                public dialog: MatDialog,
                private route: ActivatedRoute,
                public snackBar: MatSnackBar,
                private stateService: StateService,
            ) {
                this.currentUser = this.auth.currentUser;
            }

    ngOnInit() {
        this.postsSubsription = this.postService.postsSubject.subscribe(
          posts => {
            this.posts = posts;
            this.postsBackup = _.cloneDeep(posts);
          }
        );
        // this.postService.getRefrechAllPosts();
        if(this.route.snapshot.params['name'])
            this.postService.getRefrechPostsByTagName(this.filter);
        else
            this.postService.getRefrechAllPosts();
    }

    newest(){
        this.filterService.getNewest(this.filter);
        this.postService.emitAllPosts();
    }

    tagfilter(){
        this.filterService.getTagfilter(this.filter);
        this.postService.emitAllPosts();
    }

    tagunanswered(){
        this.filterService.getUnanswered(this.filter);
        this.postService.emitAllPosts();
    }

    getall(){
        this.filterService.getall();
        this.postService.emitAllPosts();
        this.filter = "";
    }

    votefilter(){
        this.filterService.getHightVote(this.filter);
        this.postService.emitAllPosts();
    }

    filterChanged(filter: string) {
        const lFilter = filter.toLowerCase();
        this.posts = _.filter(this.postsBackup, m => {
            let comments;
            m.comments.forEach(comment=>{
                comments += comment.body + " ";
            })

            let tags;
            m.tags.forEach(tag=>{
                tags += tag + " ";
            })
            const str = (m.user.pseudo + ' ' + tags + ' ' + comments).toLowerCase();
            return str.includes(lFilter);
        });
    }

    
    public addQuestion() {
        if(this.currentUser){
            const post = new Post({});
        
            const dlg = this.dialog.open(EditPostComponent, { data: { post, isNew: true, isQuestion: true }, height: "500px" });
            dlg.beforeClose().subscribe(res => {
                if (res) {
                    this.postService.add(res).subscribe(res => {
                        if (!res) {
                            this.snackBar.open(`There was an error at the server. The question has not been created! Please try again.`, 'Dismiss', { duration: 4000 });
                            this.postService.getRefrechAllPosts();
                        }else{
                            this.snackBar.open(`add question successfully`, 'Dismiss', { duration: 4000 });
                            this.postService.getRefrechAllPosts();
                        }
                    });
                }
            });
        }else{
            this.snackBar.open(`Vous devez etre connecté pour poster une question.`, 'Dismiss', { duration: 4000 });
        }
        
    }

    public ngOnDestroy(){
        this.postsSubsription.unsubscribe();
        this.snackBar.dismiss();
    }
}