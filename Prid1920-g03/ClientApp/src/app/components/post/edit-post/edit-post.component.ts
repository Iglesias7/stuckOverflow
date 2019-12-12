import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import * as _ from 'lodash';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Post } from 'src/app/models/post';
import { TagService } from 'src/app/services/tag.service';
import { PostService } from 'src/app/services/post.service';

@Component({
    selector: 'app-userCard',
    templateUrl: './edit-post.component.html',
    styleUrls: ['./edit-post.component.css'],
})




export class EditPostComponent {
    
    public editPostForm: FormGroup;
    public ctlTitle: FormControl;
    public ctlBody: FormControl;
    public questionBody: string = "";
    public isNew: boolean;
    public tags: any[];
    checkBox: boolean = false;
    

    constructor(public dialogRef: MatDialogRef<EditPostComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { post: Post; tags: any[], isNew: boolean; },
        private formBuilder: FormBuilder,
        private tagService: TagService,
        private postService: PostService
    ) {
        this.ctlTitle = this.formBuilder.control('', [Validators.required]);
        this.ctlBody = this.formBuilder.control('', [Validators.required]);
        
        this.editPostForm = this.formBuilder.group({
            title: this.ctlTitle,
            body: this.ctlBody
        });

        this.isNew = data.isNew;
        // if(this.isNew == false){
        //     this.questionBody = data.post.body;
        // }
        this.editPostForm.patchValue(data.post);

        this.tagService.getAllTags().subscribe(tags => {
            this.tags = tags;
            
            if(this.isNew == false){
                data.tags.forEach(t => {
                    this.tags.forEach(ts => {
                        if(ts.name == t){
                            ts.isChecked = true;
                        }   
                    });
                });
            }
            
        });
    }

    update() {
        const data = this.editPostForm.value;
        // data.id = data.post.id;
        // console.log(" le body est: " + this.questionBody)
        // data.body = this.questionBody;
        // data.parentId = null;
        data.authorId = this.postService.currentUser.id;
   
        data.lstags = this.tags.filter(t=>t.isChecked == true).map(m=>m.name);
        
         
        this.dialogRef.close(data);
    }

    public checkedd(name: string){
        this.tags.forEach(t => {
            if(t.name == name){
                t.isChecked = !t.isChecked;
            }   
        })
        this.checkBox = true;
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    cancel() {
        this.dialogRef.close();
    }
}