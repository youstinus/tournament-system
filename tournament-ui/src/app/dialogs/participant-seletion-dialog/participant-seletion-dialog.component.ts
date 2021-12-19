import { Component, OnInit, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';

export interface DialogData {
  animal: string;
  name: string;
}

@Component({
  selector: 'app-participant-seletion-dialog',
  templateUrl: './participant-seletion-dialog.component.html',
  styleUrls: ['./participant-seletion-dialog.component.css']
})
export class ParticipantSeletionDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<ParticipantSeletionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

    ngOnInit(){
    }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
