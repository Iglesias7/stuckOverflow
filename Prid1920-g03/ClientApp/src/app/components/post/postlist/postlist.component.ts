import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatDialog, MatSnackBar} from '@angular/material';
import * as _ from 'lodash';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';
import { Subscription } from 'rxjs';
import { EditPostComponent } from '../edit-post/edit-post.component';
import { FilterService } from 'src/app/services/filter.service';

@Component({
    selector: 'app-userCard',
    templateUrl: './postlist.component.html',
    styleUrls: ['./postlist.component.css'],
})

export class PostListComponent implements OnInit, OnDestroy {
    
    posts: Post[] = [];
    postsBackup: Post[] = [];
    postsSubsription: Subscription;

    constructor(private filterService: FilterService,private postService: PostService, public dialog: MatDialog,
        public snackBar: MatSnackBar) {}

    ngOnInit() {
        this.postsSubsription = this.postService.postsSubject.subscribe(
          posts => {
            this.posts = posts;
            this.postsBackup = _.cloneDeep(posts);
          }
        );
        
        this.postService.getRefrechAllPosts();
    }

    newest(){
        this.filterService.getNewest();
        this.postService.emitAllPosts();
    }

    tagfilter(){
        this.filterService.getTagfilter();
        this.postService.emitAllPosts();
    }

    tagunanswered(){
        this.filterService.getUnanswered();
        this.postService.emitAllPosts();
    }

    votefilter(){
        this.filterService.getHightVote();
        this.postService.emitAllPosts();
    }

    filterChanged(filter: string) {
        const lFilter = filter.toLowerCase();
        this.posts = _.filter(this.postsBackup, m => {
            const str = (m.user.pseudo + ' ' + m.tags + ' ' + m.comments).toLowerCase();
            return str.includes(lFilter);
        });
    }

    public addQuestion() {
        const post = new Post({});
        const dlg = this.dialog.open(EditPostComponent, { data: { post, isNew: true } });
        dlg.beforeClose().subscribe(res => {
            if (res) {
                console.log(" ici res: " + res.body)
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
    }

    ngOnDestroy(){
        this.postsSubsription.unsubscribe();
    }
}