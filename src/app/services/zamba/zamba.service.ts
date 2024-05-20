import { HttpClient, HttpHeaders, HttpContext } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ACLService } from '@delon/acl';
import { DA_SERVICE_TOKEN, ITokenService, ALLOW_ANONYMOUS } from '@delon/auth';
import { ALAIN_I18N_TOKEN, MenuService, SettingsService, TitleService, _HttpClient } from '@delon/theme';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import { NzIconService } from 'ng-zorro-antd/icon';
import { Observable, catchError, map, of } from 'rxjs';

import { SharedService } from './shared.service';
import { environment } from '../../../environments/environment';
import { ICONS } from '../../../style-icons';
import { ICONS_AUTO } from '../../../style-icons-auto';
import { I18NService } from '../../core/i18n/i18n.service';

@Injectable({
  providedIn: 'root'
})
export class ZambaService {
  LOGIN_URL = environment['apiRestBasePath'];

  serverError = false;
  type = 0;
  loading = false;

  constructor(
    iconSrv: NzIconService,
    private menuService: MenuService,
    @Inject(ALAIN_I18N_TOKEN) private i18n: I18NService,
    private settingService: SettingsService,
    private aclService: ACLService,
    private titleService: TitleService,
    private httpClient: HttpClient,
    private http: _HttpClient,
    private router: Router,
    public sharedService: SharedService,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private sanitizer: DomSanitizer
  ) {
    iconSrv.addIcon(...ICONS_AUTO, ...ICONS);
  }

  public getThumbsPathHome(data: any) {
    data = JSON.stringify(data);
    var url = `${this.LOGIN_URL}search/GetThumbsPathHome`;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.httpClient.post(url, data, httpOptions);
  }

  public GetUserInfoForName(data: any) {
    var url = `${this.LOGIN_URL}search/GetUserInfoForName?UserName=${data}`;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.httpClient.post(url, httpOptions);
  }

  public GetConfigUserSidbar() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};
    let groupsid: any[] = [];

    if (tokenData != null) {
      tokenData['groups'].forEach(function (values: any) {
        groupsid.push(values['ID']);
      });

            
            genericRequest = {
              UserId:  0, // tokenData['userID'],
              token: tokenData['token'],
        Params: {
          groups: groupsid.toString()
        }
      };
    }
    //TODO: este codigo carga la visualizacion de la sidbar pensar mas adelante en ponerlo asyncronico
    //actualmente no funciona de esa manera ya que recarga 2 veces la  interfaz
    const xhr = new XMLHttpRequest();
    xhr.open('POST', `${this.LOGIN_URL}/configUserSidbar`, false); // El tercer parámetro indica si la solicitud es síncrona
    xhr.setRequestHeader('Content-Type', 'application/json');

    try {
      xhr.send(JSON.stringify(genericRequest));
      if (xhr.status === 200) {
        const deserealize = JSON.parse(xhr.responseText);
        let response = JSON.parse(deserealize);

        if (response.length == 0) return true;

        let valueToAnalize = parseInt(response[0].value);

        if (valueToAnalize == 0) return true;
        else return false;
      } else {
        // Manejar errores aquí
        console.error('Error en la solicitud POST:', xhr.statusText);
        return false;
      }
    } catch (error) {
      // Manejar errores aquí
      console.error('Error en la solicitud POST:', error);
      return false;
    }
  }

  public GetSidebarItems() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};
    let groupsid: any[] = [];
    if (tokenData != null) {
      if (tokenData['groups'] != null) {
        tokenData['groups'].forEach(function (values: any) {
          groupsid.push(values['ID']);
        });

        genericRequest = {
          UserId:  0, // tokenData['userID'],
          token: tokenData['token'],
          Params: {
            groups: groupsid.toString()
          }
        };
      }
    }

    this.http
      .post(`${environment['apiRestBasePath']}/getSidebarItems`, genericRequest, null, {
        context: new HttpContext().set(ALLOW_ANONYMOUS, true)
      })
      .pipe(
        catchError(res => {
          console.warn(`Network request failed`, res);
          return of(res);
        }),
        map((appData: NzSafeAny) => {
          appData = JSON.parse(appData);
          console.log('appData: ', appData);
          if (appData) {
            this.settingService.setApp(appData.app);
            this.settingService.setUser(appData.user);
            this.aclService.setFull(true);
            this.menuService.add(appData.menu.items);
            this.titleService.default = '';
            this.titleService.suffix = appData.app.name;
          }
        })
      )
      .subscribe();
  }

  executeRule(genericRequest: any): Observable<any> {
    // Aquí realizas la lógica de tu llamada HTTP
    return this.http.post(`${environment['apiRestBasePath']}/executeRuleDashboard`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
    //return this.http.post(this.LOGIN_URL + '/executeRuleDashboard', genericRequest);
  }

  public preFlightLogin(): SafeResourceUrl {
    let tokenService = this.tokenService.get();
    console.log(tokenService);
    let userid = tokenService ? tokenService['userID'] : null;
    let token = tokenService ? tokenService['token'] : null;
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `${environment['zambaWeb']}/Views/Security/LoginRRHH.aspx?` + `c=${userid}&t=${token}`
    );
  }
}
