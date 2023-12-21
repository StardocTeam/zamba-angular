
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'environments/environment';
import { NgLocalization } from '@angular/common';


@Injectable({
  providedIn: 'root'
})
export class ZambaService {

    LOGIN_URL =  environment.urlApi;


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
