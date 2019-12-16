import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import * as _ from 'lodash';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { PostService } from 'src/app/services/post.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
    selector: 'app-userCard',
    templateUrl: './edit-comment.component.html',
    styleUrls: ['./edit-comment.component.css'],
})

export class EditCommentComponent {
    
    public editCommentForm: FormGroup;
    public ctlBody: FormControl;
    public isNew: boolean;
    

    constructor(public dialogRef: MatDialogRef<EditCommentComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { comment: Comment; isNew: boolean; },
        private formBuilder: FormBuilder,
        private postService: PostService,
        private auth: AuthenticationService
    ) {
        this.ctlBody = this.formBuilder.control('', [Validators.required]);
        
        this.editCommentForm = this.formBuilder.group({
            body: this.ctlBody
        });

        this.isNew = data.isNew;
        this.editCommentForm.patchValue(data.comment);
    }

    update() {
        const data = this.editCommentForm.value;
        data.authorId = this.auth.currentUser.id;
        this.dialogRef.close(data);
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    cancel() {
        this.dialogRef.close();
    }
}