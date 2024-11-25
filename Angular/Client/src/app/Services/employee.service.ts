import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IEmployee } from '../Models/iemployee';
import { Observable } from 'rxjs';
import { IEmployeeRequest } from '../Models/iemployee-request';
import { IEmployeeDetails } from '../Models/iemployee-details';
import { IEmployeeModel } from '../Models/employee-model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  employees: IEmployee[];
  private apiUrl = 'https://localhost:7135/api/Employee'; // Base URL for API
  constructor(private http: HttpClient) { }

  listEmployees(start?: number, size?: number): Observable<IEmployee[]> {
    // const params = new HttpParams()
    //   .set('start', start.toString())
    //   .set('size', size.toString());
    return this.http.post<IEmployee[]>(`${this.apiUrl}/ListEmployees`, null,);
  }

  deleteEmployee(id: number): Observable<void> {
    const params = new HttpParams().set('id', id.toString());
    return this.http.post<void>(`${this.apiUrl}/DeleteEmployee`, null, { params });
  }

  addEmployee(employee: IEmployeeRequest): Observable<number> {
    return this.http.post<number>(`${this.apiUrl}/AddEmployee`, employee);
  }

  updateEmployee(employee: IEmployeeDetails): Observable<number> {
    return this.http.post<number>(`${this.apiUrl}/UpdateEmployee`, employee);
  }


  getEmployee(id: number): Observable<IEmployeeDetails> {
    const params = new HttpParams().set('id', id.toString());
    return this.http.post<IEmployeeDetails>(`${this.apiUrl}/GetEmployee`, null, { params });
  }

  getEmployees(): Observable<IEmployeeModel[]> {
    return this.http.post<IEmployeeModel[]>(`${this.apiUrl}/GetEmployees`, null);
  }

  checkIfEmailUnique(email: string,id:number): Observable<boolean> {
    console.log(id)
    const params = new HttpParams()
    .set('email', email)
    .set('id', id !== undefined ? id.toString() : '');
    return this.http.post<boolean>(`${this.apiUrl}/CheckUniqueEmail`, null, { params });
  }
}