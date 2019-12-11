import { Component, OnInit, OnDestroy } from "@angular/core";
import { Tag } from "src/app/models/tag";
import { TagService } from "src/app/services/tag.service";
import { UserService } from "src/app/services/user.service";


@Component({
    selector: 'app-taglist',
    templateUrl: 'taglist.component.html',
    styleUrls: ['taglist.component.css']
})

export class TagListComponent implements OnInit {
    
    tags: Tag[] = [];

    constructor(private tagService: TagService, private userService: UserService){}

    ngOnInit() {
        
        this.tagService.getTags();
    }

    popularfilter(){

    }

    namefilter(){

    }

    newfilter(){

    }
}