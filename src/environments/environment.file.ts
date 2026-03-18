// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import * as MOCKDATA from '@_mock';

import { DelonMockModule } from '@delon/mock';
import { Environment } from '@delon/theme';

// Load runtime config injected at startup (StartupService sets `window.appConfig` from /config.json)
const runtimeConfig = (window as any).appConfig || {};
const r = (key: string, def: any) => (runtimeConfig[key] ?? def);
export const environment = {
  production: false,
  useHash: true,

  restApi: r('restApi', 'http://imageapt/Zamba.Api/api'),
  apiRestBasePath: r('apiRestBasePath', 'http://imageapt/Zamba.Api/api/Dashboard'),
  externalSearchApi: r('externalSearchApi', 'http://imageapt/Zamba.Api/api/ExternalSearch'),
  searchApi: r('searchApi', 'http://imageapt/Zamba.Api/api/search'),
  zambaWeb: r('zambaWeb', 'http://imageapt/Zamba.WebDesa'),

  cliente: 'zamba',
  api: {
    baseUrl: './',
    refreshTokenEnabled: true,
    refreshTokenType: 'auth-refresh'
  },
  modules: [DelonMockModule.forRoot({ data: MOCKDATA })]
} as Environment;
/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
