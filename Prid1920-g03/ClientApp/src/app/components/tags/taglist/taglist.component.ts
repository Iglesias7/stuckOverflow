import { Component, OnInit, OnDestroy, AfterViewInit, ViewChild } from "@angular/core"
import * as _ from 'lodash';
import { Tag } from "src/app/models/tag";
import { TagService } from "src/app/services/tag.service";
import { UserService } from "src/app/services/user.service";
import { StatementVisitor } from "@angular/compiler";
import { MatDialog, MatSnackBar, MatTableDataSource, MatPaginator, PageEvent } from "@angular/material";
import { EditTagComponent } from "../edit-tag/edit-tag.component";
import { AuthenticationService } from "src/app/services/authentication.service";
import { Role } from "src/app/models/user";
import { Subscription } from "rxjs";
import { MatTableState } from "src/app/helpers/mattable.state";
import { TagStateService } from "src/app/services/tagState.service";


@Component({
    selector: 'app-taglist',
    templateUrl: './taglist.component.html',
    styleUrls: ['./taglist.component.css'],
})

export class TagListComponent implements OnInit, OnDestroy {
    
    
        
    tags: Tag[] = [];
    tagsBackup: Tag[] = [];
    tagSubscription: Subscription;

    length: number = 0;
    pageSize: number = 3;
    pageSizeOptions: number[] = [3, 6, 9, 12];

    dataSources: MatTableDataSource<Tag> = new MatTableDataSource();
    dataSource: Tag[] = [];
    filter: string;
    state: MatTableState;

    @ViewChild(MatPaginator, { static: false}) paginator: MatPaginator;

    constructor(private tagService: TagService, 
        private userService: UserService,
        private stateService: TagStateService,
        public dialog: MatDialog,
        public snackBar: MatSnackBar,
        public auth: AuthenticationService
        ){
            this.state = this.stateService.TagListState;
    }

    ngOnInit(): void {
        this.tagService.getAllTags().subscribe(tags => {
            this.tags = tags;
            this.tagsBackup = _.cloneDeep(tags);
            this.dataSources.data = this.tags.slice(0,3);
            this.length = this.tags.length;
        }); 
        this.refresh();
    }

    refresh() {
        this.tagService.getRefreshAllTags();
    }
    onPageChange(event: PageEvent){
        let startIndex = event.pageIndex * event.pageSize;
        let endIndex = startIndex + event.pageSize;
        if(endIndex > this.length){
            endIndex = this.length;
        }
        this.dataSources.data = this.tags.slice(startIndex, endIndex);
    }

    edit(tag: Tag) {
        const dlg = this.dialog.open(EditTagComponent, { data: {tag, isNew: false}});
        dlg.beforeClose().subscribe(res => {
            if (res) {
                const tagId = tag.id;
                _.assign(tag, res);
                this.tagService.update(res, tagId).subscribe(res => {
                    if (!res) {
                        this.snackBar.open(`There was an error at the server. The update has not been done! Please try again.`, 'Dismiss', { duration: 10000 });
                        this.refresh();
                    }
                });
            }
        });
    }

    delete(tag: Tag) {
        const snackBarRef = this.snackBar.open(`Tag '${tag.name}' will be deleted`, 'Undo', { duration: 10000 });
        snackBarRef.afterDismissed().subscribe(res => {
            if (!res.dismissedByAction)
                this.tagService.delete(tag).subscribe();
                this.refresh();
        });
    }

    create() {
        const tag = new Tag({});
        const dlg = this.dialog.open(EditTagComponent, { data: { tag, isNew: true } });
        dlg.beforeClose().subscribe(res => {
            console.log(res.name);
            if (res) {
                this.tagService.add(res).subscribe(res => {
                    if (!res) {
                        this.snackBar.open(`There was an error at the server. The tag has not been created! Please try again.`, 'Dismiss', { duration: 10000 });
                    }
                    this.refresh();
                });
            }
        });
    }

     filterChanged(filter: string) {
        const lFilter = filter.toLowerCase();
        this.tags = _.filter(this.tagsBackup, tg => {
            const str = (tg.name).toLowerCase();
            return str.includes(lFilter);
        });
        this.dataSources.data = this.tags.slice(0,3);
    }

    get currentUser() {
        return this.auth.currentUser;
      }
      
      get isAdmin() {
        return this.currentUser && this.currentUser.role === Role.Admin;
      }



    popularfilter(){
        this.tagService.getByNbPosts().subscribe(tags => {
            this.tags = tags;
            this.tagsBackup = _.cloneDeep(tags);
            this.dataSources.data = this.tags;

        });
    }



    namefilter(){
        this.refresh();
    }

    newfilter(){
        this.tagService.getByTimestamp().subscribe(tags => {
            this.tags = tags;
            this.tagsBackup = _.cloneDeep(tags);
            this.dataSources.data = this.tags;
        });
    }

    ngOnDestroy(): void {
        this.snackBar.dismiss();
    }
}