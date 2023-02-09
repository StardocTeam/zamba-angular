import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Globals } from 'app/main/global';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root',
})
export class EditService {
  LOGIN_URL =  environment.urlApi;
  //LOGIN_URL = 'https://bpm.provinciaseguros.com.ar/zambadesa.restapi/ExternalSearch/Login';

  //EXAMPLE_URL = 'https://restcountries.eu/rest/v2/all';
  constructor(private httpClient: HttpClient) { }
  GlobalVars = new Globals();


  public getEntities(data) {

    var url = this.LOGIN_URL + "search/GetAllEntities";
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.httpClient.post(url, data, httpOptions);
    //return this.httpClient.get(this.LOGIN_URL);
  }

  public getIndexForEntities(data) {
    data = JSON.stringify(data);
    var url = this.LOGIN_URL + "search/GetIndexForEntities";
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.httpClient.post(url, data, httpOptions);
    //return this.httpClient.get(this.LOGIN_URL);
  }


  public TEST_SaveImapProcess(data) {

    data = JSON.stringify(data);

    var url = this.LOGIN_URL + "eInbox/SaveImapProcess";

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.httpClient.post(url, data, httpOptions);
  }



    SaveImapProcess(data) {
      return this.httpClient.post(this.LOGIN_URL + "eInbox/SaveImapProcess", JSON.stringify(data), this.newHttpHeaders());
    }

  getEMails(ImapObj) {
    return this.httpClient.post(this.LOGIN_URL + "eInbox/GetEmails", ImapObj, this.newHttpHeaders());
  }

  getConection(ConectionObj) {
    return this.httpClient.post(this.LOGIN_URL + "eInbox/GetConection", ConectionObj, this.newHttpHeaders());
  }

  insertMail(MailObj) {
    return this.httpClient.post(this.GlobalVars.ApiUrl + "eInbox/InsertEmailsInZamba", MailObj, this.newHttpHeaders());
  }

  private newHttpHeaders() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
  }
}