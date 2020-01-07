import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {MatAutocompleteSelectedEvent, MatAutocomplete} from '@angular/material/autocomplete';
import {MatChipInputEvent} from '@angular/material/chips';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { Component, Inject, ViewChild, ElementRef } from '@angular/core';
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
    // d = this.ctlBody;
    public isNew: boolean;
    public isQuestion: boolean;
    public tags: any[];


    visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  fruitCtrl = new FormControl();
  filteredFruits: Observable<string[]>;
  fruits: string[] = ['Lemon'];
  allFruits: string[] = ['Apple', 'Lemon', 'Lime', 'Orange', 'Strawberry'];

  @ViewChild('fruitInput', {static: false}) fruitInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto', {static: false}) matAutocomplete: MatAutocomplete;
    
    constructor(public dialogRef: MatDialogRef<EditPostComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { post: Post; tags: any[], isNew: boolean, isQuestion: boolean; },
        private formBuilder: FormBuilder,
        private tagService: TagService,
        private auth: AuthenticationService

    ) {

        this.filteredFruits = this.fruitCtrl.valueChanges.pipe(
            startWith(null),
            map((fruit: string | null) => fruit ? this._filter(fruit) : this.allFruits.slice()));

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



    add(event: MatChipInputEvent): void {
        // Add fruit only when MatAutocomplete is not open
        // To make sure this does not conflict with OptionSelected Event
        if (!this.matAutocomplete.isOpen) {
          const input = event.input;
          const value = event.value;
    
          // Add our fruit
          if ((value || '').trim()) {
            this.fruits.push(value.trim());
          }
    
          // Reset the input value
          if (input) {
            input.value = '';
          }
    
          this.fruitCtrl.setValue(null);
        }
      }
    
      remove(fruit: string): void {
        const index = this.fruits.indexOf(fruit);
    
        if (index >= 0) {
          this.fruits.splice(index, 1);
        }
      }
    
      selected(event: MatAutocompleteSelectedEvent): void {
        this.fruits.push(event.option.viewValue);
        this.fruitInput.nativeElement.value = '';
        this.fruitCtrl.setValue(null);
      }
    
      private _filter(value: string): string[] {
        const filterValue = value.toLowerCase();
    
        return this.allFruits.filter(fruit => fruit.toLowerCase().indexOf(filterValue) === 0);
      }
}