import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeListComponent } from './Components/employee-list/employee-list.component';
import { EmployeeFormComponent } from './Components/employee-form/employee-form.component';
import { AuditListComponent } from './Components/audit-list/audit-list.component';
import { LoginComponent } from './Components/login/login.component';
import { AuthGuard } from './Gaurd/auth.guard';
import { RoleGuard } from './Gaurd/role.guard';
import { AlreadyLoggedInGuard } from './Gaurd/already-logged-in.guard';

const routes: Routes = [
  {path:'',redirectTo:'Home',pathMatch:'full'},
  {path:'Login',component:LoginComponent,canActivate: [AlreadyLoggedInGuard]},
  {path:'Home',component:EmployeeListComponent,canActivate: [AuthGuard],  },
  {path:'Employee/Edit/:id',component:EmployeeFormComponent,canActivate: [AuthGuard],},
  {path:'Employee/Create',component:EmployeeFormComponent,canActivate: [AuthGuard],},
  {path:'Audit',component:AuditListComponent,canActivate: [AuthGuard,RoleGuard],},
  { path: '**', redirectTo: 'Home' }, // Fallback to Home if no route matches



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
