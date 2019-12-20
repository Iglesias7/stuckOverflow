import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatDialog, MatSnackBar} from '@angular/material';
import * as _ from 'lodash';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post';
import { Subscription } from 'rxjs';
import { EditPostComponent } from '../edit-post/edit-post.component';
import { FilterService } from 'src/app/services/filter.service';
import { ActivatedRoute } from '@angular/router';


@Component({
    selector: 'app-userCard',
    templateUrl: './postlistbytag.component.html',
    styleUrls: ['./postlistbytag.component.css'],
})

export class PostListByTagComponent implements OnInit, OnDestroy {
    
    posts: any;
    postsBackup: any;
    postsSubsription: Subscription;
    demo: string = null;
    researchByTag: boolean = false;

    
    constructor(private filterService: FilterService,private route: ActivatedRoute,private postService: PostService, public dialog: MatDialog,
        public snackBar: MatSnackBar) {
            // this.getElem();
        }



    
    ngOnInit(): void  {
        // this.getElem();
    }

    

    // public getElem(){
    //     const name = this.route.snapshot.params['name'];
    //     this.postService.getPostsByTagName(name).subscribe(posts => {
    //         this.posts = posts;
    //         this.postsBackup = _.cloneDeep(posts);
    //         console.log(posts);         
    //         if(!posts){
    //             this.researchByTag = true;
    //         }
    //     });
    //     this.postService.emitPost();
    // }


    

    newest(){
        this.filterService.getNewest(this.demo);
        this.postService.emitPost();
    }

    tagfilter(){
        this.filterService.getTagfilter(this.demo);
        this.postService.emitPost();
    }

    tagunanswered(){
        this.filterService.getUnanswered(this.demo);
        this.postService.emitPost();
    }

    votefilter(){
        this.filterService.getHightVote();
        this.postService.emitPost();
    }

    filterChanged(filter: string) {
        const lFilter = filter.toLowerCase();
        this.posts = _.filter(this.postsBackup, m => {
            const str = (m.postUser.pseudo + ' ' + m.tags + ' ' + m.comments).toLowerCase();
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