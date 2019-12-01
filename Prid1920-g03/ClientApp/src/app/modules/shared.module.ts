import { NgModule } from '@angular/core';
import {
  MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule,
  MatTableModule, MatPaginatorModule, MatSortModule, MatIconModule,
  MatSlideToggleModule, MatDialogModule, MatSnackBarModule, MatTabsModule, MatDividerModule, 
  MatSelectModule, MatCardModule, MatMenuModule, MatListModule, MatTooltipModule, MatExpansionModule
} from '@angular/material';


@NgModule({
  imports: [
    MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule,
    MatTableModule, MatPaginatorModule, MatSortModule, MatIconModule,
    MatSlideToggleModule, MatDialogModule, MatSnackBarModule, MatTabsModule, MatDividerModule, 
    MatSelectModule, MatCardModule, MatMenuModule, MatListModule, MatTooltipModule, MatExpansionModule 
  ],
  exports: [
    MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule,
    MatTableModule, MatPaginatorModule, MatSortModule, MatIconModule,
    MatSlideToggleModule, MatDialogModule, MatSnackBarModule, MatTabsModule,
    MatSelectModule, MatCardModule, MatMenuModule, MatListModule, MatTooltipModule, MatExpansionModule, MatDividerModule 
  ],
})
export class SharedModule { }