// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
import * as MOCKDATA from '@_mock';
import { DelonMockModule } from '@delon/mock';
import { Environment } from '@delon/theme';
export const environment = {
  production: false,
  useHash: true,
  //#region Ambiente Obsoleto (don widgets)
  //apiRestBasePath: 'https://www.zamba.com.ar/zambaweb.restapi/api/Dashboard',
  //externalSearchApi: 'https://www.zamba.com.ar/zambaweb.restapi/api/ExternalSearch',
  //zambaWeb: 'https://www.zamba.com.ar/Zamba.Web',
  //#endregion

  //#region Ambiente de TEST
  apiRestBasePath: 'http://imageapt/ZambaAngularTEST.Restapi/api/Dashboard',
  externalSearchApi: 'http://imageapt/ZambaAngularTEST.Restapi/api/ExternalSearch',
  searchApi: 'http://imageapt/ZambaAngularTEST.Restapi/api/search',
  //#endregion

  //#region Ambiente de DESA
  // apiRestBasePath: 'http://imageapt/Zamba.Api/api/Dashboard',
  // externalSearchApi: 'http://imageapt/Zamba.Api/api/ExternalSearch',
  // searchApi: 'http://imageapt/Zamba.Api/api/search',
  zambaWeb: 'http://imageapt/zamba.webDESA',
  //#endregion
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
