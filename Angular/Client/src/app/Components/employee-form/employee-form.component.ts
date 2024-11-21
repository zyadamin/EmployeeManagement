import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { IEmployeeDetails } from 'src/app/Models/iemployee-details';
import { IEmployeeRequest } from 'src/app/Models/iemployee-request';
import { IProject } from 'src/app/Models/iproject';
import { IProjectRequest } from 'src/app/Models/iproject-request';
import { EmployeeService } from 'src/app/Services/employee.service';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { projectNameValidator, uniqueEmailValidator } from 'src/app/Validators/custom-validators';
import { uptime } from 'process';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css']
})
export class EmployeeFormComponent implements OnInit {

  employeeForm: FormGroup;
  employeeId: string | null = null;
  isEditMode:boolean;
  employee:IEmployeeDetails;
  constructor(private fb: FormBuilder,
    private employeeService: EmployeeService,
    private router: Router,
    private route: ActivatedRoute,
    private dialog: MatDialog,
  ) {

    this.employeeForm = this.fb.group({
      name: new FormControl('', [Validators.required]),
      email: new FormControl('', {validators:[Validators.required,Validators.email],
        asyncValidators:[uniqueEmailValidator(this.employeeService)],
        updateOn:'blur'}),
      projects: this.fb.array([], { validators: projectNameValidator })

    });
  }

  checkEmail(){
    console.log("checkEmail");

    if(this.employee && this.email.value != this.employee.email){
      if(!this.email.asyncValidator){
        this.email.setAsyncValidators(uniqueEmailValidator(this.employeeService));
        this.email.updateValueAndValidity();
      }
    }
    else{this.email.clearAsyncValidators();}
  }

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      this.employeeId = params.get('id');
      if (this.employeeId) {
        this.isEditMode = true;
        this.email.setAsyncValidators([]);
        this.getEmployee(Number(this.employeeId));
      }
    });
  }

  get Projects(): FormArray {
    return this.employeeForm.get("projects") as FormArray
  }

  get name() {
    return this.employeeForm.get('name');
  }

  get email() {
    return this.employeeForm.get('email');
  }

  newProject(): FormGroup {
    return this.fb.group({
      id: ['0'],
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      startDate: ['', [Validators.required]],
      endDate: ['', []],
    })
  }

  addProject() {
    this.Projects.push(this.newProject());
  }

  removeProject(i: number) {
    this.Projects.removeAt(i);
  }


  openSubmitConfirmation() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: { message: `Are you sure you want save?` }
    });

    dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {

        if (this.employeeId) {
          let employeeRequest: IEmployeeDetails = {
            id: Number(this.employeeId),
            email: this.email.value,
            name: this.name.value,
            projects: this.Projects.value.map((project: IProject) => ({
              id: project.id,
              name: project.name,
              description: project.description,
              startDate:project.startDate,
              endDate:project.endDate
            })),
          }

          this.employeeService.updateEmployee(employeeRequest).subscribe({
            next: (data: number) => {
              this.router.navigate(['/Home']);
            },
            error: (err) => {
              console.error('Error add employee:', err);
            }
          });
        }
        else {
          let employeeRequest: IEmployeeRequest = {
            email: this.email.value,
            name: this.name.value,
            projects: this.Projects.value.map((project: IProjectRequest) => ({
              name: project.name,
              description: project.description,
              startDate:project.startDate,
              endDate:project.endDate
            })),
          }

          this.employeeService.addEmployee(employeeRequest).subscribe({
            next: (data: number) => {
              this.router.navigate(['/Home']);
            },
            error: (err) => {
              console.error('Error add employee:', err);
            }
          });
        }

      }
    });

  }

  getEmployee(id: number): void {
    this.employeeService.getEmployee(id).subscribe({
      next: (employee: IEmployeeDetails) => {
        this.employee=employee;
        this.employeeForm.patchValue({
          name: employee.name,
          email: employee.email,
        });

        // Populate projects
        employee.projects.forEach((project) => {
          this.Projects.push(
            this.fb.group({
              id: [project.id],
              name: [project.name, Validators.required],
              description: [project.description, Validators.required],
              startDate:[project.startDate, Validators.required],
              endDate:[project.endDate],
            })
          );
        });

      },
      error: (err) => {
        console.error('Error add employee:', err);

      }
    });
  }
  
}


