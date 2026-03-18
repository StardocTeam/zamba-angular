import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ACLService } from '@delon/acl';
import { ALAIN_I18N_TOKEN, MenuService, SettingsService, TitleService } from '@delon/theme';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import { NzIconService } from 'ng-zorro-antd/icon';
import { Observable, zip, catchError, map } from 'rxjs';

import { ICONS } from '../../../style-icons';
import { ICONS_AUTO } from '../../../style-icons-auto';
import { I18NService } from '../i18n/i18n.service';

/**
 * Used for application startup
 * Generally used to get the basic data of the application, like: Menu Data, User Data, etc.
 */
@Injectable()
export class StartupService {
  constructor(
    iconSrv: NzIconService,
    private menuService: MenuService,
    @Inject(ALAIN_I18N_TOKEN) private i18n: I18NService,
    private settingService: SettingsService,
    private aclService: ACLService,
    private titleService: TitleService,
    private httpClient: HttpClient,
    private router: Router
  ) {
    iconSrv.addIcon(...ICONS_AUTO, ...ICONS);
  }

  load(): Observable<void> {
    const defaultLang = this.i18n.defaultLang;
    // Try to load runtime config from /config.json (root). If not available, fall back to assets/config.json
    const config$ = this.httpClient.get('/config.json').pipe(catchError(() => this.httpClient.get('assets/config.json')));
    // If http request allows anonymous access, you need to add `ALLOW_ANONYMOUS`:
    // this.httpClient.get('assets/tmp/app-data.json', { context: new HttpContext().set(ALLOW_ANONYMOUS, true) })
    return (zip(this.i18n.loadLangData(defaultLang), this.httpClient.get('assets/tmp/app-data.json'), config$) as Observable<[Record<string, string>, NzSafeAny, any]>).pipe(
      catchError((res: any) => {
        console.warn(`StartupService.load: Network request failed`, res);
        setTimeout(() => this.router.navigateByUrl(`/exception/500`));
        // Throw error to stop execution on startup failure
        throw res;
      }),
      map(([langData, appData, config]: [Record<string, string>, NzSafeAny, any]) => {
        // setting language data
        this.i18n.use(defaultLang, langData);

        // expose runtime config to window.appConfig for other code to use
        try {
          (window as any).appConfig = config || {};
        } catch (e) {
          console.warn('Unable to set window.appConfig', e);
        }

        //this.settingService.setApp(appData.app);
        //this.settingService.setUser(appData.user);
        //this.aclService.setFull(true);
        //this.menuService.add(appData.menu);
        //this.titleService.default = '';
        //this.titleService.suffix = appData.app.name;
      })
    );
  }
}
