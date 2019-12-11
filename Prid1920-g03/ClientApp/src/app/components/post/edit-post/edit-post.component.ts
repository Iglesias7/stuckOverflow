import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import * as _ from 'lodash';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Post } from 'src/app/models/post';
import { TagService } from 'src/app/services/tag.service';
import { PostService } from 'src/app/services/post.service';
import { CheckedTag } from 'src/app/models/checkedTag';
import { Tag } from 'src/app/models/tag';

@Component({
    selector: 'app-userCard',
    templateUrl: './edit-post.component.html',
    styleUrls: ['./edit-post.component.css'],
})




export class EditPostComponent {
    
    public editPostForm: FormGroup;
    public ctlTitle: FormControl;
    public questionBody: string = "dd";
    public isNew: boolean;
    public tags: any[];
    

    constructor(public dialogRef: MatDialogRef<EditPostComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { post: Post; isNew: boolean; },
        private formBuilder: FormBuilder,
        private tagService: TagService,
        private postService: PostService
    ) {
        this.ctlTitle = this.formBuilder.control('', [Validators.required]);
        
        this.editPostForm = this.formBuilder.group({
            title: this.ctlTitle
        });

        this.isNew = data.isNew;
        
        this.editPostForm.patchValue(data.post);

        this.tagService.getAllTags().subscribe(tags => {
            this.tags = tags;
           
        });
    }

    update() {
        const data = this.editPostForm.value;
        data.body = this.questionBody;
        // data.parentId = null;
        data.authorId = this.postService.currentUser.id;
   
        data.lstags = this.tags.filter(t=>t.isChecked == true).map(m=>m.name);
        console.log(" data " + this.data.post.authorId );
        this.dialogRef.close(data);
    }

    public checked(name: string){
        this.tags.forEach(t => {
            if(t.name == name){
                t.isChecked = !t.isChecked;
            }   
        })
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    cancel() {
        this.dialogRef.close();
    }
}