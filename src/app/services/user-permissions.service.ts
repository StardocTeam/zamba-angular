import { HttpContext } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS, DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { catchError, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserPermissionsService {
  private readonly storageKey = 'user_permissions';
  Permissions: RightsColection = new RightsColection();

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private http: _HttpClient,
  ) {
    const token = this.tokenService.get();

    if (token && token['userID']) {
      this.loadFromSessionStorage();
    }
  }

  getPermissions(): RightsColection {
    this.loadFromSessionStorage();
    return this.Permissions;
  }

  setPermissions(data: RightsColection): void {
    this.Permissions.rights = Array.isArray(data.rights) ? data.rights : [];
    this.Permissions.userId = data.userId;

    try {
      sessionStorage.setItem(`${this.storageKey}_${this.Permissions.userId}`, JSON.stringify(this.Permissions));
    } catch {
      // ignore storage errors
    }
  }

  clearPermissions(): void {
    this.Permissions = new RightsColection();
    try {
      const token = this.tokenService.get();

      if (token && token['userID']) {
        sessionStorage.removeItem(`${this.storageKey}_${token['userID']}`);
      }
    } catch {
      // ignore storage errors
    }
  }

  private loadFromSessionStorage(): void {
    try {
      const token = this.tokenService.get();

      if (token && token['userID']) {
        const raw = sessionStorage.getItem(`${this.storageKey}_${token['userID']}`);

        if (raw) {
          const parsed = JSON.parse(raw) as RightsColection;
          this.Permissions = Array.isArray(parsed.rights) ? { userId: parsed.userId, rights: parsed.rights } : new RightsColection();
        }
      }
    } catch {
      this.Permissions = new RightsColection();
    }
  }

  getAllUserPermissions() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: 0,
        token: tokenData['token'],
        Params: {},
      };

      this.http
        .post(`${environment['apiRestBasePath']}/getUserAllPermissions`, genericRequest, null, {
          context: new HttpContext().set(ALLOW_ANONYMOUS, true),
        })
        .pipe(
          catchError(res => {
            console.warn(`Network request failed`, res);
            return of(res);
          }),
          map((data: RightsColection) => {
            const permissions = data;
            this.setPermissions(permissions);
            return permissions;
          }),
        )
        .subscribe();
    }
  }
}

class RightsColection {
  userId: number = 0;
  rights: UserPermissions[] = [];
}

class UserPermissions {
  ADITIONAL: number = 0;
  GROUPID: number = 0;
  OBJID: number = 0;
  RTYPE: number = 0;
}
