import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { MatDialog, MatSnackBar} from '@angular/material';
import * as _ from 'lodash';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';
import { Subscription } from 'rxjs';
import { EditPostComponent } from '../edit-post/edit-post.component';
import { FilterService } from 'src/app/services/filter.service';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/user';
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
    filter: string;
    demo: string = this.route.snapshot.params['name'];

    
    constructor(private auth: AuthenticationService,
        private filterService: FilterService,
        private route: ActivatedRoute,
        private postService: PostService, 
        public dialog: MatDialog,
        public snackBar: MatSnackBar,
        private stateService: StateService,
        ) {
            this.currentUser = this.auth.currentUser;
        }



    
    ngOnInit(): void  {
        const name = this.route.snapshot.params['name'];
        this.postsSubsription = this.postService.postsSubject.subscribe(
            posts => {
              this.posts = posts;
              this.postsBackup = _.cloneDeep(posts);
            }
        );
        this.postService.getRefrechPostsByTagName(name);

    }

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
    }

    clear(){
        const name = this.route.snapshot.params['name'];
        this.postsSubsription = this.postService.postsSubject.subscribe(
            posts => {
              this.posts = posts;
              this.postsBackup = _.cloneDeep(posts);
            }
        );
        this.postService.getRefrechPostsByTagName(name);
    }

    public addQuestion() {
        const post = new Post({});
        const dlg = this.dialog.open(EditPostComponent, { data: { post, isNew: true, isQuestion: true }, height: "500px" });
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