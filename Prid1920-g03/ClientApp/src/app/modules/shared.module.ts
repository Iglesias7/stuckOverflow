import { NgModule } from '@angular/core';
import {
  MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule, MatButtonToggleModule,
  MatTableModule, MatPaginatorModule, MatSortModule, MatIconModule, MatToolbarModule, MatChipsModule,
  MatSlideToggleModule, MatDialogModule, MatSnackBarModule, MatTabsModule, MatDividerModule, 
  MatSelectModule, MatCardModule, MatMenuModule, MatListModule, MatTooltipModule, MatExpansionModule, MatAutocompleteModule
} from '@angular/material';


@NgModule({
  imports: [
    MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule, MatButtonToggleModule,
    MatTableModule, MatPaginatorModule, MatSortModule, MatIconModule, MatToolbarModule, MatChipsModule,
    MatSlideToggleModule, MatDialogModule, MatSnackBarModule, MatTabsModule, MatDividerModule, 
    MatSelectModule, MatCardModule, MatMenuModule, MatListModule, MatTooltipModule, MatExpansionModule, MatAutocompleteModule
  ],
  exports: [
    MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule, MatChipsModule,
    MatTableModule, MatPaginatorModule, MatSortModule, MatIconModule, MatToolbarModule,
    MatSlideToggleModule, MatDialogModule, MatSnackBarModule, MatTabsModule, MatButtonToggleModule, MatAutocompleteModule,
    MatSelectModule, MatCardModule, MatMenuModule, MatListModule, MatTooltipModule, MatExpansionModule, MatDividerModule
  ],
})
export class SharedModule { }