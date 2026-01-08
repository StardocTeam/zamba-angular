import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@env/environment';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  GetAllGroups() {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      "UserId": 0,
      "token": "",
      "Params": {

      }
    };
    return this.http.post(
      `${this.serviceBase}Admin/GetAllGroups`,
      genericRequest,
      { headers }
    );
  }

  GetAllUsersForAGroup(groupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      "UserId": 0,
      "token": "",
      "Params": {
        "groupId": groupId
      }
    };
    return this.http.post(
      `${this.serviceBase}Admin/GetAllUsersForAGroup`,
      genericRequest,
      { headers }
    );
  }

  GetAllUsers() {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      "UserId": 0,
      "token": "",
      "Params": {

      }
    };
    return this.http.post(
      `${this.serviceBase}Admin/GetAllUsers`,
      genericRequest,
      { headers }
    );
  }

  RemoveUserFromGroup(userId: number, groupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      "UserId": 0,
      "token": "",
      "Params": {
        "userId": userId,
        "groupId": groupId
      }
    };
    return this.http.post(
      `${this.serviceBase}Admin/RemoveUserFromGroup`,
      genericRequest,
      { headers }
    );
  }

  AddUserIntoGroup(userId: number, groupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      "UserId": 0,
      "token": "",
      "Params": {
        "userId": userId,
        "groupId": groupId
      }
    };
    return this.http.post(
      `${this.serviceBase}Admin/AddUserIntoGroup`,
      genericRequest,
      { headers }
    );
  }
  AddInheritedGroup(groupId: number, inheritedGroupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      "UserId": 0,
      "token": "",
      "Params": {
        "groupId1": groupId,
        "groupId2": inheritedGroupId
      }
    };
    return this.http.post(
      `${this.serviceBase}Admin/AddInheritedGroup`,
      genericRequest,
      { headers }
    );
  }

  DeleteInheritedGroup(groupId: number, inheritedGroupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      "UserId": 0,
      "token": "",
      "Params": {
        "groupId1": groupId,
        "groupId2": inheritedGroupId
      }
    };
    return this.http.post(
      `${this.serviceBase}Admin/DeleteInheritedGroup`,
      genericRequest,
      { headers }
    );
  }

  GetInheritedGroups(groupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      "UserId": 0,
      "token": "",
      "Params": {
        "groupId": groupId
      }
    };
    return this.http.post(
      `${this.serviceBase}Admin/GetInheritedGroups`,
      genericRequest,
      { headers }
    );
  }


  private serviceBase: string = '';
  constructor(private http: HttpClient) {
    let restAPIUrl = `${environment['apiRestBasePath']}`.toLocaleLowerCase();
    restAPIUrl = restAPIUrl.replace('dashboard', '');
    this.serviceBase = restAPIUrl;
  }

}
