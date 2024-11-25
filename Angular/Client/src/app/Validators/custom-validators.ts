import { AbstractControl, AsyncValidatorFn, ValidationErrors, ValidatorFn } from "@angular/forms";
import { EmployeeService } from "../Services/employee.service";
import { Observable } from "rxjs";
import { map } from 'rxjs/operators';


export function projectNameValidator(control: AbstractControl):ValidationErrors | null{

        const projects = control.value;
        if (!projects || !Array.isArray(projects)) {
          return null; // Return null if no projects exist
        }
      
        const projectNames = projects.map((project: any) => (project.name));
        
        const hasDuplicates = projectNames.some(
          (name, index) => projectNames.indexOf(name) !== index
        );
      
        return hasDuplicates ? { duplicateProjectNames: true } : null;
    };


    export function uniqueEmailValidator(employeeService: EmployeeService,id?:number): AsyncValidatorFn {
        return (control: AbstractControl): Observable<ValidationErrors | null> => {

          console.log("zysad")
          if (!control.value) {
            return null;
          }
      
          return employeeService
          .checkIfEmailUnique(control.value,id)
          .pipe(map((result: boolean) => result ?  null :{ emailAlreadyExists: true }));
        };
      }