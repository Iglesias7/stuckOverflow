import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { MatDialog, MatSnackBar, MatTableDataSource, MatPaginator, PageEvent} from '@angular/material';
import * as _ from 'lodash';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';
import { Subscription } from 'rxjs';
import { EditPostComponent } from '../edit-post/edit-post.component';
import { FilterService } from 'src/app/services/filter.service';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/user';
import { MatListPostState } from 'src/app/helpers/matListPost.state';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { StateService } from 'src/app/services/state.service';


@Component({
    selector: 'app-userCard',
    templateUrl: './postlistbytag.component.html',
    styleUrls: ['./postlistbytag.component.css'],
})

export class PostListByTagComponent implements OnInit, OnDestroy {
    
    currentUser: User;
    posts: Post[] = [];
    postsBackup: Post[] = [];
    postsSubsription: Subscription;
    researchByTag: boolean = false;

    // length: number = 0;
    // pageSize: number = 3;  
    // pageSizeOptions: number[] = [3, 6, 9, 12, 15, 18, 21];

    // dataSources: MatTableDataSource<Post> = new MatTableDataSource();
    filter: string;
    // state: MatListPostState;

    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

    
    constructor(private auth: AuthenticationService,
        private filterService: FilterService,
        private route: ActivatedRoute,
        private postService: PostService, 
        public dialog: MatDialog,
        public snackBar: MatSnackBar,
        private stateService: StateService,
        ) {
            this.currentUser = this.auth.currentUser;
            // this.state = this.stateService.postListState;
        }



    
    ngOnInit(): void  {
        const name = this.route.snapshot.params['name'];
        this.postsSubsription = this.postService.postsSubject.subscribe(
            posts => {
              this.posts = posts;
              this.postsBackup = _.cloneDeep(posts);

            //   this.dataSources.data = this.posts.slice(0, 3);
            //   this.length = this.posts.length
            }
        );
        this.postService.getRefrechPostsByTagName(name);

    }

    // onPageChange(event: PageEvent){
    //     let startIndex = event.pageIndex * event.pageSize;
    //     let endIndex = startIndex + event.pageSize;
    //     if(endIndex > this.length){
    //       endIndex = this.length;
    //     }
    //     this.dataSources.data = this.posts.slice(startIndex, endIndex);
    // }

    

    newest(){
        const name = this.route.snapshot.params['name'];
        this.filterService.getNewestByTag(name);
        this.postService.emitPost();
    }

    
    tagunanswered(){
        const name = this.route.snapshot.params['name'];
        this.filterService.getUnansweredByTag(name);
        this.postService.emitPost();
    }

    votefilter(){
        const name = this.route.snapshot.params['name'];
        this.filterService.getHightVoteByTag(name);
        this.postService.emitPost();
    }

    filterChanged(filter: string) {
        const lFilter = filter.toLowerCase();
        this.posts = _.filter(this.postsBackup, m => {
            const str = (m.user.pseudo + ' ' + m.tags + ' ' + m.title + ' ' + m.comments).toLowerCase();
            return str.includes(lFilter);
        });
        // this.dataSources.data = this.posts.slice(0, 3);

    }

    clear(){
        const name = this.route.snapshot.params['name'];
        this.postsSubsription = this.postService.postsSubject.subscribe(
            posts => {
              this.posts = posts;
              this.postsBackup = _.cloneDeep(posts);

            //   this.dataSources.data = this.posts.slice(0, 3);
            //   this.length = this.posts.length
            }
        );
        this.postService.getRefrechPostsByTagName(name);
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
                        this.postService.emitPost();
                    }else{
                        this.snackBar.open(`add question successfully`, 'Dismiss', { duration: 4000 });
                        this.postService.emitPost();
                    }
                });
            }
        });
    }

    ngOnDestroy(): void{
        this.postsSubsription.unsubscribe();
    }
}