import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { IEmployeeDetails } from 'src/app/Models/iemployee-details';

@Component({
  selector: 'app-changes-pop-up',
  templateUrl: './changes-pop-up.component.html',
  styleUrls: ['./changes-pop-up.component.css']
})
export class ChangesPopUpComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<ChangesPopUpComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { oldData:any,newData:any }
  ) {}

  ngOnInit() {
  }

}
