// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
import * as MOCKDATA from '@_mock';
import { DelonMockModule } from '@delon/mock';
import { Environment } from '@delon/theme';
export const environment = {
    production: false,
    useHash: true,
    appPrimaryColor: '#0077ff',

    restApi: 'http://localhost/ZambaCESRestApiDesa/api',
    apiRestBasePath: 'http://localhost/ZambaCESRestApiDesa/api/Dashboard',
    charts: 'http://localhost/ZambaCESRestApiDesa/api/charts',
    externalSearchApi: 'http://localhost/ZambaCESRestApiDesa/api/ExternalSearch',
    searchApi: 'http://localhost/ZambaCESRestApiDesa/api/search',
    zambaWeb: 'http://localhost/ZambaCESDesa',

    cliente: 'zamba',
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
