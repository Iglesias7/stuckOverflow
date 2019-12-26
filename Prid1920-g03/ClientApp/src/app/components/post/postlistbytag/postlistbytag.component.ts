import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatDialog, MatSnackBar, MatTableDataSource} from '@angular/material';
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

    dataSource: MatTableDataSource<Post> = new MatTableDataSource<Post>(this.posts);
    filter: string;
    state: MatListPostState;
    
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



    
    ngOnInit(): void  {
        this.getElem();
    }

    

    public getElem(){
        const name = this.route.snapshot.params['name'];
        // this.postService.getPostsByTagName(name).subscribe(posts => {
        //     this.posts = posts;
        //     this.postsBackup = _.cloneDeep(posts);
        //     console.log(posts);         
        //     if(!posts){
        //         this.researchByTag = true;
        //     }
        // });
        // this.postService.emitPost();

        this.postsSubsription = this.postService.postsSubject.subscribe(
            posts => {
              this.posts = posts;
              this.postsBackup = _.cloneDeep(posts);
            }
        );
        this.postService.getRefrechPostsByTagName(name);


    }


    

    newest(){
        this.filterService.getNewest(this.filter);
        this.postService.emitPost();
    }

    tagfilter(){
        this.filterService.getTagfilter(this.filter);
        this.postService.emitPost();
    }

    tagunanswered(){
        this.filterService.getUnanswered(this.filter);
        this.postService.emitPost();
    }

    votefilter(){
        this.filterService.getHightVote();
        this.postService.emitPost();
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