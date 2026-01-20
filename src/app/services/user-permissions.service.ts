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
    Permissions: any[] = [];

    constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService, private http: _HttpClient,) {
        //this.getAllUserPermissions();
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
                    map((data: any) => {
                        //TODO: asignar data al servicio para que toda la app tenga acceso a esta informacion.
                    })
                )
                .subscribe();

        }
    }
}
