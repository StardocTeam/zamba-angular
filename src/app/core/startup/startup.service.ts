import { HttpClient } from '@angular/common/http';
import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ACLService } from '@delon/acl';
import { ALAIN_I18N_TOKEN, MenuService, SettingsService, TitleService } from '@delon/theme';
import { LazyService } from '@delon/util/other';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import { NzIconService } from 'ng-zorro-antd/icon';
import { Observable, zip, catchError, map, of } from 'rxjs';

import { ICONS } from '../../../style-icons';
import { ICONS_AUTO } from '../../../style-icons-auto';
import { I18NService } from '../i18n/i18n.service';

/**
 * Used for application startup
 * Generally used to get the basic data of the application, like: Menu Data, User Data, etc.
 */
@Injectable()
export class StartupService {
  private lessReady: Promise<void> | null = null;

  constructor(
    iconSrv: NzIconService,
    private menuService: MenuService,
    @Inject(ALAIN_I18N_TOKEN) private i18n: I18NService,
    private settingService: SettingsService,
    private aclService: ACLService,
    private titleService: TitleService,
    private httpClient: HttpClient,
    private router: Router,
    private lazy: LazyService,
    @Inject(DOCUMENT) private doc: Document,
  ) {
    iconSrv.addIcon(...ICONS_AUTO, ...ICONS);
  }

  load(): Observable<void> {
    const defaultLang = this.i18n.defaultLang;
    // Runtime config: `config.json` is copied from `src/assets/config.json` into the
    // `appSettings/` folder at the root of the deployed dist (sibling of `assets/`),
    // so it can be edited/overridden per environment without touching the assets bundle.
    const config$ = this.httpClient.get('appSettings/config.json').pipe(catchError(() => of({})));
    // If http request allows anonymous access, you need to add `ALLOW_ANONYMOUS`:
    // this.httpClient.get('assets/tmp/app-data.json', { context: new HttpContext().set(ALLOW_ANONYMOUS, true) })
    return (
      zip(this.i18n.loadLangData(defaultLang), this.httpClient.get('assets/tmp/app-data.json'), config$) as Observable<
        [Record<string, string>, NzSafeAny, any]
      >
    ).pipe(
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

        // Same mechanism ng-alain's own `setting-drawer` uses to switch the theme color at
        // runtime (color.less + less.js + `less.modifyVars`), driven by config.json instead
        // of the drawer's color picker.
        this.applyThemeColor(config?.appPrimaryColor);

        //this.settingService.setApp(appData.app);
        //this.settingService.setUser(appData.user);
        //this.aclService.setFull(true);
        //this.menuService.add(appData.menu);

        this.titleService.default = 'Zamba';
        this.titleService.prefix = 'Zamba';
        //this.titleService.suffix = appData.app.name;
      }),
    );
  }

  private loadLess(): Promise<void> {
    if (!this.lessReady) {
      this.lessReady = this.lazy
        .loadStyle('assets/color.less', { rel: 'stylesheet/less' })
        .then(() => {
          const script = this.doc.createElement('script');
          script.innerHTML = `window.less = { async: true, env: 'production', javascriptEnabled: true };`;
          this.doc.body.appendChild(script);
        })
        .then(() => this.lazy.loadScript('assets/less.min.js'))
        .then(() => undefined);
    }
    return this.lessReady;
  }

  private applyThemeColor(primaryColor: string | undefined): void {
    if (!primaryColor) {
      return;
    }
    this.loadLess()
      .then(() => (window as any).less.modifyVars({ '@primary-color': primaryColor }))
      .catch(e => console.warn('Unable to apply appPrimaryColor from config.json', e));
  }
}
