import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILoginModel } from '../Models/ilogin-model';
import { IToken } from '../Models/itoken';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl = 'https://localhost:7192/api/Authenticate'; // Base URL for API
  constructor(private http: HttpClient, private router: Router) { }

  Login(loginModel: ILoginModel): boolean {

    this.http.post<IToken>(`${this.apiUrl}/login`, loginModel).subscribe({
      next: (data: IToken) => {

        let token = JSON.parse(window.atob(data.token.split('.')[1]));

        localStorage.setItem('isLoggedIn', 'true');
        localStorage.setItem('userRole', token.Role);
        localStorage.setItem('Token', token);
        this.router.navigate(['/Home']);
        return false;
      },
      error: (err) => {
        console.error('Error loading order:', err);
      }
    });

    return true;
  }

  Logout() {
    localStorage.removeItem('isLoggedIn');
    localStorage.removeItem('userRole');
    localStorage.removeItem('Token');
    this.router.navigate(['/Login']);
  }

  IsLoggedIn(): boolean {

    let isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';;
    if (isLoggedIn) {
      return true;
    }
    return false
  }

  IsAuthorized() :boolean{
    return localStorage.getItem('userRole') === 'Audit';
  }

}
