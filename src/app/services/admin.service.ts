import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  GetAllGroups() {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {},
    };
    return this.http.post(`${this.serviceBase}Admin/GetAllGroups`, genericRequest, { headers });
  }

  GetAllUsersForAGroup(groupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {
        groupId: groupId,
      },
    };
    return this.http.post(`${this.serviceBase}Admin/GetAllUsersForAGroup`, genericRequest, { headers });
  }

  GetAllUsers() {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {},
    };
    return this.http.post(`${this.serviceBase}Admin/GetAllUsers`, genericRequest, { headers });
  }

  RemoveUserFromGroup(userId: number, groupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {
        userId: userId,
        groupId: groupId,
      },
    };
    return this.http.post(`${this.serviceBase}Admin/RemoveUserFromGroup`, genericRequest, { headers });
  }

  AddUserIntoGroup(userId: number, groupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {
        userId: userId,
        groupId: groupId,
      },
    };
    return this.http.post(`${this.serviceBase}Admin/AddUserIntoGroup`, genericRequest, { headers });
  }
  AddInheritedGroup(groupId: number, inheritedGroupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {
        groupId1: groupId,
        groupId2: inheritedGroupId,
      },
    };
    return this.http.post(`${this.serviceBase}Admin/AddInheritedGroup`, genericRequest, { headers });
  }

  DeleteInheritedGroup(groupId: number, inheritedGroupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {
        groupId1: groupId,
        groupId2: inheritedGroupId,
      },
    };
    return this.http.post(`${this.serviceBase}Admin/DeleteInheritedGroup`, genericRequest, { headers });
  }

  GetInheritedGroups(groupId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {
        groupId: groupId,
      },
    };
    return this.http.post(`${this.serviceBase}Admin/GetInheritedGroups`, genericRequest, { headers });
  }
  GetGroupsForAUser(userId: number) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {
        userId: userId,
      },
    };
    return this.http.post(`${this.serviceBase}Admin/GetGroupsForAUser`, genericRequest, { headers });
  }
  updateGroup(groupId: number, groupData: { name: string; description: string }): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {
        groupId: groupId,
        name: groupData.name,
        description: groupData.description,
      },
    };
    return this.http.post(`${this.serviceBase}Admin/UpdateGroup`, genericRequest, { headers });
  }

  updateUserData(userData: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: userData,
    };

    const url = `${this.serviceBase}Admin/UpdateUserData`;
    return this.http.post(url, genericRequest, { headers });
  }

  changePassword(userId: number, username: string, newPassword: string): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {
        ID: userId.toString(),
        Usuario: username,
        NewPassword: newPassword,
      },
    };

    const url = `${this.serviceBase}Admin/ChangePassword`;
    return this.http.post(url, genericRequest, { headers });
  }

  getDataTypes(): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {},
    };

    const url = `${this.serviceBase}Admin/GetDataTypesForAdicionalUserData`;
    return this.http.post(url, genericRequest, { headers });
  }

  getAdditionalData(userId: number): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {
        ID: userId.toString(),
      },
    };

    const url = `${this.serviceBase}Admin/GetAditionalUserData`;
    return this.http.post(url, genericRequest, { headers });
  }

  saveAdditionalDataType(name: string): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    var genericRequest: any = {
      UserId: 0,
      token: '',
      Params: {
        DataTypeName: name,
      },
    };

    const url = `${this.serviceBase}Admin/SaveAdditionalDataType`;
    return this.http.post(url, genericRequest, { headers });
  }

  private serviceBase: string = '';
  constructor(private http: HttpClient) {
    let restAPIUrl = `${environment['apiRestBasePath']}`.toLocaleLowerCase();
    restAPIUrl = restAPIUrl.replace('dashboard', '');
    this.serviceBase = restAPIUrl;
  }
}
