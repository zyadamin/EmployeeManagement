import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { IEmployee } from 'src/app/Models/iemployee';
import { EmployeeService } from 'src/app/Services/employee.service';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees: IEmployee[] = [];
  columns = ['Name', 'Email'];
  actions = ['Edit', 'Delete'];

  constructor(private employeeService: EmployeeService,
    private dialog: MatDialog,
    private router: Router) { }

  ngOnInit() {
    this.fetchEmployees();
  }

  fetchEmployees() {
    this.employeeService.listEmployees().subscribe((data: IEmployee[]) => {
      this.employees = data;
    });
  }

  handleAction(event: { action: string, row: any }) {

    if (event.action === 'Edit') {

      this.router.navigate(['/Employee/Edit', event.row.id]);
    }
    else if (event.action === 'Delete') {
      this.openDeleteConfirmation(event.row);
    }
  }

  openDeleteConfirmation(employee: IEmployee) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: { message: `Are you sure you want to delete ${employee.name}?` }
    });

    dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.employeeService.deleteEmployee(employee.id).subscribe(() => {
          this.fetchEmployees();
        });
      }
    });

  }
}
