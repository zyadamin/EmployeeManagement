import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeListComponent } from './Components/employee-list/employee-list.component';
import { EmployeeFormComponent } from './Components/employee-form/employee-form.component';
import { AuditListComponent } from './Components/audit-list/audit-list.component';

const routes: Routes = [
  {path:'',redirectTo:'Home',pathMatch:'full'},
  {path:'Home',component:EmployeeListComponent},
  {path:'Employee/Edit/:id',component:EmployeeFormComponent},
  {path:'Employee/Create',component:EmployeeFormComponent},
  {path:'Audit',component:AuditListComponent},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
