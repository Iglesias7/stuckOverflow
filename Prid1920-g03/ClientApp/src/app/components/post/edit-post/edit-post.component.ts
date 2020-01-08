import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatAutocompleteSelectedEvent, MatAutocomplete } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Component, Inject, ViewChild, ElementRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import * as _ from 'lodash';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
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

  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  tagsCtrl = new FormControl();
  filteredtags: Observable<string[]>;
  lsTag: string[] = [];
  tagsName: string[];

  @ViewChild('tagsInput', { static: false }) tagsInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto', { static: false }) matAutocomplete: MatAutocomplete;

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
      tags: this.tagsCtrl
    });

    this.isNew = data.isNew;
    this.isQuestion = data.isQuestion;
    this.editPostForm.patchValue(data.post);

    if (this.isQuestion)
      this.tagService.getAllTags().subscribe(tags => {
        this.tags = tags;
        this.tagsName = tags.map(t=>t.name)

        if(!this.isNew)
          this.tagsName.forEach((tag) => {
            if(this.data.tags.find(t => t == tag) ){
              this.lsTag.push(tag)
              var pos = this.tagsName.indexOf(tag);
              this.tagsName.splice(pos, 1)
            }
          })

        this.filteredtags = this.tagsCtrl.valueChanges.pipe(
          startWith(null),
          map((tag: string | null) => tag ? this._filter(tag) : this.tagsName.slice())
        );
      });
  }

  update() {
    const data = this.editPostForm.value;
    data.authorId = this.auth.currentUser.id;
    if (this.isQuestion == true)
      data.tags = this.lsTag;

    this.dialogRef.close(data);
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  public cancel() {
    this.dialogRef.close();
  }

  add(event: MatChipInputEvent): void {
    // Add tag only when MatAutocomplete is not open
    // To make sure this does not conflict with OptionSelected Event
    if (!this.matAutocomplete.isOpen) {
      const input = event.input;
      const value = event.value;
      // Add our tag
      if ((value || '').trim()) {
        this.lsTag.push(value.trim());
      }

      // Reset the input value
      if (input) {
        input.value = '';
      }

      this.tagsCtrl.setValue(null);
    }
  }

  remove(tag: string): void {
    const index = this.lsTag.indexOf(tag);

    if (index >= 0) {
      this.lsTag.splice(index, 1);
    }
    this.tagsName.push(tag)
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.lsTag.push(event.option.viewValue);
    var pos = this.tagsName.indexOf(event.option.viewValue);
    this.tagsName.splice(pos, 1)
    this.tagsInput.nativeElement.value = '';
    this.tagsCtrl.setValue(null);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.tagsName.filter(tag => tag.toLowerCase().indexOf(filterValue) === 0);
  }
}