// Single build for all environments: values come from `config.json`, loaded at
// startup by `StartupService` into `window.appConfig` (see `startup.service.ts`).
// No `--configuration` flag or `fileReplacements` are needed for a normal build;
// just edit `config.json` next to the deployed `dist` for each environment.
import * as MOCKDATA from '@_mock';
import { DelonMockModule } from '@delon/mock';
import { Environment } from '@delon/theme';

const runtimeConfig = (window as any).appConfig || {};
const r = (key: string, def: any) => runtimeConfig[key] ?? def;

export const environment = {
  production: r('production', true),
  useHash: true,
  appPrimaryColor: r('appPrimaryColor', '#c36200'),

  restApi: r('restApi', ''),
  apiRestBasePath: r('apiRestBasePath', ''),
  charts: r('charts', ''),
  externalSearchApi: r('externalSearchApi', ''),
  searchApi: r('searchApi', ''),
  zambaWeb: r('zambaWeb', ''),

  cliente: r('cliente', 'zamba'),

  api: {
    baseUrl: './',
    refreshTokenEnabled: true,
    refreshTokenType: 'auth-refresh',
  },
  modules: [DelonMockModule.forRoot({ data: MOCKDATA })],
} as Environment;
/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
