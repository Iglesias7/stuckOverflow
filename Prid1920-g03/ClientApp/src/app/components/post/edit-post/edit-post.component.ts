import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import * as _ from 'lodash';
import { FormBuilder, FormGroup, Validators, FormControl, FormArray } from '@angular/forms';
import { Post } from 'src/app/models/post';
import { TagService } from 'src/app/services/tag.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
    selector: 'app-userCard',
    templateUrl: './edit-post.component.html',
    styleUrls: ['./edit-post.component.css'],
})

export class EditPostComponent {

    public editPostForm: FormGroup;
    public ctlTitle: FormControl;
    public ctlBody: FormControl;
    public isNew: boolean;
    public isQuestion: boolean;
    public tags: any[];
    
    constructor(public dialogRef: MatDialogRef<EditPostComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { post: Post; tags: any[], isNew: boolean, isQuestion: boolean; },
        private formBuilder: FormBuilder,
        private tagService: TagService,
        private auth: AuthenticationService

    ) {
        this.ctlTitle = this.formBuilder.control('', [Validators.required]);
        this.ctlBody = this.formBuilder.control('', [Validators.required]);
        
        this.editPostForm = this.formBuilder.group({
            title: this.ctlTitle,
            body: this.ctlBody,
            tagForms: new FormArray([])
        });

        this.isNew = data.isNew;
        this.isQuestion = data.isQuestion;
        this.editPostForm.patchValue(data.post);

        if(this.isQuestion == true)
            this.tagService.getAllTags().subscribe(tags => {
                this.tags = tags;
                
                this.tags.forEach((o, i) => {
                    if(this.isNew == true){
                        const control = new FormControl();
                        (this.editPostForm.controls.tagForms as FormArray).push(control)
                    }else{
                        const control = new FormControl(this.data.tags.find(t => t == o.name )? true : false);   
                        (this.editPostForm.controls.tagForms as FormArray).push(control) 
                    }
                })
            });
    }

    update() {
        const data = this.editPostForm.value;
        data.authorId = this.auth.currentUser.id;
        if(this.isQuestion == true)
            data.tags = this.editPostForm.value.tagForms.map((tf, i) => tf? this.tags[i] : null).filter(tf => tf != null).map(t => t.name);
        this.dialogRef.close(data);
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    cancel() {
        this.dialogRef.close();
    }
}