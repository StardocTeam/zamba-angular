import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  
  LOGIN_URL = environment.urlApi + 'Account/LoginImap';
  

  constructor(private httpClient: HttpClient) { }

  
    public Login(data){
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type':  'application/json'
        })
      };
      return this.httpClient.post(this.LOGIN_URL,data,httpOptions);
    }


    public DecryptUserName(data){

      var urlDecryptUserName = environment.urlApi + 'search/DecryptUser';
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type':  'application/json'
        })
      };
      return this.httpClient.post(urlDecryptUserName,data,httpOptions);
    }

}
