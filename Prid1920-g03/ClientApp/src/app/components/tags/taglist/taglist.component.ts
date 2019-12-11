import { Component, OnInit, OnDestroy, AfterViewInit } from "@angular/core"
import * as _ from 'lodash';
import { Tag } from "src/app/models/tag";
import { TagService } from "src/app/services/tag.service";
import { UserService } from "src/app/services/user.service";
import { StatementVisitor } from "@angular/compiler";
import { StateService } from "src/app/services/state.service";
import { MatDialog, MatSnackBar } from "@angular/material";
import { EditTagComponent } from "../edit-tag/edit-tag.component";


@Component({
    selector: 'app-taglist',
    templateUrl: './taglist.component.html',
    styleUrls: ['./taglist.component.css'],
})

export class TagListComponent implements AfterViewInit, OnDestroy {
    ngOnDestroy(): void {
        throw new Error("Method not implemented.");
    }
    ngAfterViewInit(): void {
        this.refresh();
    }
    
    tags: Tag[] = [];
    tagsBackup: Tag[] = [];

    filter: string;

    constructor(private tagService: TagService, private userService: UserService,
        private stateService: StateService,
        public dialog: MatDialog,
        public snackBar: MatSnackBar){}

    
    


    refresh() {
        this.tagService.getAllTags().subscribe(tags => {
            this.tags = tags;
            this.tagsBackup = _.cloneDeep(tags);
        });
    }

    edit(tag: Tag) {
        const dlg = this.dialog.open(EditTagComponent, { data: {tag, isNew: false}});
        dlg.beforeClose().subscribe(res => {
            if (res) {
                _.assign(tag, res);
                this.tagService.update(res).subscribe(res => {
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
        });
    }

    create() {
        const tag = new Tag({});
        const dlg = this.dialog.open(EditTagComponent, { data: { tag, isNew: true } });
        dlg.beforeClose().subscribe(res => {
            if (res) {
                this.tagService.add(res).subscribe(res => {
                    if (!res) {
                        this.snackBar.open(`There was an error at the server. The tag has not been created! Please try again.`, 'Dismiss', { duration: 10000 });
                        this.refresh();
                    }
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
    }



    popularfilter(){

    }



    namefilter(){

    }

    newfilter(){

    }
}