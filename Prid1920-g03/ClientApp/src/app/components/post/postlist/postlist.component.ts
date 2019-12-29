import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { MatDialog, MatSnackBar, MatTableDataSource, MatPaginator, PageEvent} from '@angular/material';
import * as _ from 'lodash';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';
import { Subscription } from 'rxjs';
import { EditPostComponent } from '../edit-post/edit-post.component';
import { FilterService } from 'src/app/services/filter.service';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { User } from 'src/app/models/user';
import { MatListPostState } from 'src/app/helpers/matListPost.state';
import { StateService } from 'src/app/services/state.service';

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

    length: number = 0;
    pageSize: number = 3;  
    pageSizeOptions: number[] = [3, 6, 9, 12];

    dataSources: MatTableDataSource<Post> = new MatTableDataSource();
    dataSource: Post[]= [];
    filter: string;
    state: MatListPostState;

    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

    constructor(private auth: AuthenticationService, 
                private filterService: FilterService,
                private postService: PostService, 
                public dialog: MatDialog,
                public snackBar: MatSnackBar,
                private stateService: StateService,
            ) {
                this.currentUser = this.auth.currentUser;
                this.state = this.stateService.postListState;                
            }

    ngOnInit() {
        this.postsSubsription = this.postService.postsSubject.subscribe(
          posts => {
            this.posts = posts;
            this.postsBackup = _.cloneDeep(posts);
            
            this.dataSources.data = this.posts.slice(0, 3);
            this.length = this.posts.length;
          }
        );
        this.postService.getRefrechAllPosts();
    }

    OnPageChange(event: PageEvent){
        let startIndex = event.pageIndex * event.pageSize;
        let endIndex = startIndex + event.pageSize;
        if(endIndex > this.length){
          endIndex = this.length;
        }
        this.dataSources.data = this.posts.slice(startIndex, endIndex);
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
        this.filterService.getall(this.filter);
        this.postService.emitAllPosts();
    }

    votefilter(){
        this.filterService.getHightVote(this.filter);
        this.postService.emitAllPosts();
    }

    filterChanged(filter: string) {
        const lFilter = filter.toLowerCase();
        this.posts = _.filter(this.postsBackup, m => {
            const str = (m.user.pseudo + ' ' + m.tags + ' ' + m.title + ' ' + m.comments).toLowerCase();
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
    }
}