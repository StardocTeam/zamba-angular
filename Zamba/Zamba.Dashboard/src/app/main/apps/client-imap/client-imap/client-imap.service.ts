import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Globals } from 'app/main/Global';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClientImapService {
  GlobalVars = new Globals();
  E_INBOX_URL = environment.urlApi;

  constructor(private httpClient: HttpClient) {}

    
   public getListTable(){
    var data = {};
    var url = this.E_INBOX_URL + "eInbox/ImportProcessImap";
     const httpOptions = {
       headers: new HttpHeaders({
         'Content-Type':  'application/json'
       })
     };
     return this.httpClient.post( url,data,httpOptions);
   }

   DeleteProcess(data){
    console.log("servicio delete process de angular");

    data = JSON.stringify(data);
      var url = this.E_INBOX_URL + "eInbox/DeleteProcessImap";
       const httpOptions = {
         headers: new HttpHeaders({
           'Content-Type':  'application/json'
         })
       };
       return this.httpClient.post( url,data,httpOptions);
   }

  insertMail(UserId) {
    return this.httpClient.post(this.GlobalVars.ApiUrl + "eInbox/InsertEmailsInZamba", UserId, this.newHttpHeaders());
  }

   SetProcessActiveState(data){
    console.log("servicio update state process de angular");
    data = JSON.stringify(data);
      var url = this.E_INBOX_URL + "eInbox/SetProcessActiveState";
       const httpOptions = {
         headers: new HttpHeaders({
           'Content-Type':  'application/json'
         })
       };
       return this.httpClient.post(url,data,httpOptions);
   }

   private newHttpHeaders() {
    return {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        })
    };
}   
   
}
