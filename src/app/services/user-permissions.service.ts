import { HttpContext } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS, DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { catchError, map, of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class UserPermissionsService {
    private readonly storageKey = 'user_permissions';
    Permissions: UserPermissions[] = [];

    constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService, private http: _HttpClient,) {
        this.loadFromSessionStorage();
    }

    getPermissions(): any[] {
        return this.Permissions;
    }

    setPermissions(data: UserPermissions[] = []): void {
        debugger;
        this.Permissions = Array.isArray(data) ? data : [];
        try {
            sessionStorage.setItem(this.storageKey, JSON.stringify(this.Permissions));
        } catch {
            // ignore storage errors
        }
    }

    private loadFromSessionStorage(): void {
        try {
            const raw = sessionStorage.getItem(this.storageKey);
            if (raw) {
                const parsed = JSON.parse(raw);
                this.Permissions = Array.isArray(parsed) ? parsed : [];
            }
        } catch {
            this.Permissions = [];
        }
    }

    getAllUserPermissions() {
        const tokenData = this.tokenService.get();
        let genericRequest = {};

        if (tokenData != null) {
            genericRequest = {
                UserId: 0,
                token: tokenData['token'],
                Params: {}
            };

            this.http
                .post(`${environment['apiRestBasePath']}/getUserAllPermissions`, genericRequest, null, {
                    context: new HttpContext().set(ALLOW_ANONYMOUS, true)
                })
                .pipe(
                    catchError(res => {
                        console.warn(`Network request failed`, res);
                        return of(res);
                    }),
                    map((data: UserPermissions[]) => {
                        const permissions = data;
                        this.setPermissions(permissions);
                        return permissions;
                    })
                )
                .subscribe();

        }
    }
}

class UserPermissions {
    ADITIONAL: number = 0;
    GROUPID: number = 0;
    OBJID: number = 0;
    RTYPE: number = 0;
}