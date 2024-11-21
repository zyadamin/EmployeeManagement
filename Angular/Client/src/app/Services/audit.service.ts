import { Injectable } from '@angular/core';
import { IAudit } from '../Models/iaudit';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuditService {

  audits: IAudit[];
  private apiUrl = 'https://localhost:7091/api/Audit'; // Base URL for API
  constructor(private http:HttpClient) { }

  listAudits(employeeId:number): Observable<IAudit[]> {
        const params = new HttpParams()
      .set('employeeId', employeeId.toString())
    return this.http.post<IAudit[]>(`${this.apiUrl}/GetEmployeeAudit`,null,{params});
  }

}
