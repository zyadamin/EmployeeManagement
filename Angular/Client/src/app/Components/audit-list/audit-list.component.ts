import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { IAudit } from 'src/app/Models/iaudit';
import { IEmployeeDetails } from 'src/app/Models/iemployee-details';
import { AuditService } from 'src/app/Services/audit.service';
import { ChangesPopUpComponent } from '../changes-pop-up/changes-pop-up.component';
import { IEmployeeModel } from 'src/app/Models/employee-model';
import { EmployeeService } from 'src/app/Services/employee.service';

@Component({
  selector: 'app-audit-list',
  templateUrl: './audit-list.component.html',
  styleUrls: ['./audit-list.component.css']
})
export class AuditListComponent implements OnInit {

  columns = ['Action', 'Timestamp', 'Name'];
  actions = ['Open'];
  audits: IAudit[];
  employees:IEmployeeModel[];

  constructor(private auditService: AuditService,
    private dialog: MatDialog,
    private employeeService:EmployeeService
  ) { }

  ngOnInit() {
    this.fetchEmployees();
   // this.fetchAudits(19);
  }

  fetchAudits(employeeId: number) {
    this.auditService.listAudits(employeeId).subscribe((data: IAudit[]) => {
      this.audits = data;
      console.log(this.audits)
    });
  }

  fetchEmployees() {
    this.employeeService.getEmployees().subscribe((data: IEmployeeModel[]) => {
      this.employees = data;
      console.log(this.audits)
    });
  }
  onEmployeeChange(event: Event): void {

    const selectElement = event.target as HTMLSelectElement;
      this.fetchAudits(Number(selectElement.value));
  }

  handleAction(event: { action: string, row: any }) {

    if (event.action === 'Open') {
      let oldData: any = JSON.parse(event.row.oldData);
      let newData: any = JSON.parse(event.row.newData);


      this.openChangesPopUp(oldData,newData);

    }
  }
  openChangesPopUp(oldData: any, newData: any) {
    const dialogRef = this.dialog.open(ChangesPopUpComponent, {
      width: '600px', // Set width
      height: '400px', // Set height
      data: { oldData: oldData, newData: newData }
    });
  }
}
