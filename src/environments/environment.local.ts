// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
import * as MOCKDATA from '@_mock';
import { DelonMockModule } from '@delon/mock';
import { Environment } from '@delon/theme';
export const environment = {
  production: false,
  useHash: true,
  apiRestBasePath: 'http://localhost/ZambaWeb.RestApi/api/Dashboard',
  externalSearchApi: 'http://localhost/ZambaWeb.RestApi/api/ExternalSearch',
  zambaWeb: 'http://localhost/Zamba.Web',
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
