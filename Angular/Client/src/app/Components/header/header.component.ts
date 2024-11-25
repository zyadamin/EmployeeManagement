import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private logService:LoginService) { }


  ngOnInit() {
  }

  logout(){
    this.logService.Logout();
  }

  isLoggedIn() :boolean{
    return this.logService.IsLoggedIn();
  }

  isAuthorized() :boolean{
    return this.logService.IsAuthorized();
  }

}
