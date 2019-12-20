import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { MatDialog, MatSnackBar, MatTableDataSource, MatPaginator} from '@angular/material';
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

    dataSource: MatTableDataSource<Post> = new MatTableDataSource<Post>(this.posts);
    filter: string;
    state: MatListPostState;

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
                this.state = this.stateService.postListState;
            }

    ngOnInit() {
        // lie le datasource au paginator
        // this.dataSource.paginator = this.paginator;

        // // définit le predicat qui doit être utilisé pour filtrer les membres
        // this.dataSource.filterPredicate = (data: Post, filter: string) => {
        //     const str = data.title + ' ' + data.user.pseudo + " " + data.tags + ' ' + data.comments;
        //     return str.toLowerCase().includes(filter);
        // };

        // // établit les liens entre le data source et l'état de telle sorte que chaque fois que 
        // // le tri ou la pagination est modifié l'état soit automatiquement mis à jour
        // this.state.bind(this.dataSource);

        
        this.postsSubsription = this.postService.postsSubject.subscribe(
          posts => {
            this.posts = posts;
            this.postsBackup = _.cloneDeep(posts);
          }
        );
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
        this.filterService.getall(this.filter);
        this.postService.emitAllPosts();
    }

    votefilter(){
        this.filterService.getHightVote();
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
    }

    public ngOnDestroy(){
        this.postsSubsription.unsubscribe();
    }
}