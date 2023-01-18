import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private router: Router) { }

  validateTokenExists(){
    
    var userToken = "";
    var rv = false;
    userToken = localStorage.getItem("token");
    console.log("user token:" + userToken);
    if(userToken != "" && userToken != undefined)
      rv = true;
    else
      this.router.navigate(["pages/auth/login"]);
    return rv;

  }

}
