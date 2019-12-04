import { Component, OnInit, ViewEncapsulation, AfterViewInit, ElementRef, OnDestroy, Input } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog, MAT_DIALOG_DATA, MatListItem, MatSnackBar, PageEvent, MatSortHeader } from '@angular/material';
import * as _ from 'lodash';
import { FormBuilder, FormGroup, Validators, FormControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';
import { UserService } from 'src/app/services/user.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-userCard',
    templateUrl: './postlist.component.html',
    styleUrls: ['./postlist.component.css'],
})

export class PostListComponent implements OnInit, OnDestroy {
    
    posts: Post[] = [];
    postsBackup: Post[] = [];
    postsSubsription: Subscription;
    demo = null;

    constructor(private postService: PostService, private userService: UserService,) {}

    ngOnInit() {
        this.postsSubsription = this.postService.postsSubject.subscribe(
          posts => {
            this.posts = posts;
            this.postsBackup = _.cloneDeep(posts);
          }
        );
        
        this.postService.getPosts();
        this.postService.emitPost();
    }

    newest(){
        this.postService.getNewest();
        this.postService.emitPost();
    }

    tagfilter(){
        this.postService.getTagfilter();
        this.postService.emitPost();
    }

    tagunanswered(){
        this.postService.getUnanswered();
        this.postService.emitPost();
    }

    votefilter(){
        this.postService.getHightVote();
        this.postService.emitPost();
    }

    filterChanged(filter: string) {
        const lFilter = filter.toLowerCase();
        this.posts = _.filter(this.postsBackup, m => {
            const str = (m.user.pseudo + ' ' + m.tags + ' ' + m.comments).toLowerCase();
            return str.includes(lFilter);
        });
    }

    ngOnDestroy(){
        this.postsSubsription.unsubscribe();
    }
}