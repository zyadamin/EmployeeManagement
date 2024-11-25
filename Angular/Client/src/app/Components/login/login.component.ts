import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ILoginModel } from 'src/app/Models/ilogin-model';
import { IToken } from 'src/app/Models/itoken';
import { LoginService } from 'src/app/Services/login.service';
import {jwtDecode} from 'jwt-decode';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  faildLogin:boolean = false;

  constructor(private fb: FormBuilder, 
    private loginService: LoginService,
    private router:Router) {

    this.loginForm = this.fb.group({
      email: new FormControl('', { validators: [Validators.required, Validators.email] }),
      password: new FormControl('', { validators: [Validators.required] }),
    });
  }

  ngOnInit() {
  }

  login() {
    let loginModel: ILoginModel = {
      email: this.email.value,
      password: this.password.value
    }

    this.faildLogin = this.loginService.Login(loginModel);
  }

  logout(){
    this.loginService.Logout();
  }
  
  get email() {
    return this.loginForm.get("email");

  }

  get password() {

    return this.loginForm.get("password");

  }

}
