import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, AsyncValidatorFn } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import * as _ from 'lodash';
import { User, Role } from 'src/app/models/user';
import { Tag } from 'src/app/models/tag';
import { TagService } from 'src/app/services/tag.service';


@Component({
    selector: 'app-edit-tag-mat',
    templateUrl: './edit-tag.component.html',
    styleUrls: ['./edit-tag.component.css']
})

export class EditTagComponent {
    public editForm: FormGroup;
    public ctlTagName: FormControl;
    public isNew : boolean;

    constructor(public dialogRef: 
        MatDialogRef<EditTagComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { tag: Tag; isNew: boolean;},
        private formbuilder: FormBuilder,
        private tagService: TagService
        ){
            this.ctlTagName = this.formbuilder.control('', [Validators.required], [this.tagAlreadyExist()]);
            this.editForm = this.formbuilder.group({
                name: this.ctlTagName
            });
            console.log(data);
            this.isNew = data.isNew;
            this.editForm.patchValue(data.tag);
    }

    tagAlreadyExist(): AsyncValidatorFn {
        let timeout: NodeJS.Timer;
        return (ctl: FormControl) => {
            clearTimeout(timeout);
            const name = ctl.value;
            return new Promise(resolve => {
                timeout = setTimeout(() => {
                    if (ctl.pristine) {
                        resolve(null);
                    } else{
                        this.tagService.getTagByName(name).subscribe(tag => {
                            resolve(tag ? null : { tagUsed: true } );
                        });
                    }
                }, 300);
            });
        };
    }


    onNoClick(): void {
        this.dialogRef.close();
    }
    update() {
        this.dialogRef.close(this.editForm.value);
    }
    cancel() {
        this.dialogRef.close();
    }
    
}