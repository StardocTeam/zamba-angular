import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class Style1Service {

LOGIN_URL = environment.urlApi;


  constructor(private httpClient: HttpClient) { }

  
    public getThumbsPathHome(data){
      data = JSON.stringify(data);
      var url = this.LOGIN_URL + "search/GetThumbsPathHome";
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type':  'application/json'
        })
      };
       return this.httpClient.post(url,data,httpOptions);
    }

    

    public GetUserInfoForName(data){
      var url = this.LOGIN_URL + "search/GetUserInfoForName?UserName=" + data;
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type':  'application/json'
        })
      };
       return this.httpClient.post(url,httpOptions);
    }
}
