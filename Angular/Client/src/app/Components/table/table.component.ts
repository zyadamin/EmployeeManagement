import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {

  constructor() { }


  ngOnInit() {
  }

  @Input() columns: string[] = []; // Table column names
  @Input() data: any[] = []; // Table data
  @Input() actions: string[] = []; // Actions like Edit/Delete

  @Output() actionClicked = new EventEmitter<{ action: string, row: any }>();

  onAction(action: string, row: any) {
    this.actionClicked.emit({ action, row });
  }

}
