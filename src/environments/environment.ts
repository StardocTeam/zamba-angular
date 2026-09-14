// Single build for all environments: values come from `config.json`, loaded at
// startup by `StartupService` into `window.appConfig` (see `startup.service.ts`).
// No `--configuration` flag or `fileReplacements` are needed for a normal build;
// just edit `config.json` next to the deployed `dist` for each environment.
import * as MOCKDATA from '@_mock';
import { DelonMockModule } from '@delon/mock';
import { Environment } from '@delon/theme';

// Read lazily on each access: `window.appConfig` is only populated once the async
// `StartupService.load()` (APP_INITIALIZER) resolves, which happens after this module
// is first imported, so a plain object literal would freeze the pre-load defaults.
const r = (key: string, def: any) => ((window as any).appConfig?.[key] ?? def);

// Kept as a separate static export (not a getter): AOT/ngtsc must statically evaluate
// `environment.modules` for `@NgModule` metadata, which isn't possible on an object
// literal that also contains get accessors.
export const environmentModules = [DelonMockModule.forRoot({ data: MOCKDATA })];

export const environment = {
  get production() { return r('production', true); },
  useHash: true,
  get appPrimaryColor() { return r('appPrimaryColor', '#c36200'); },

  get restApi() { return r('restApi', ''); },
  get apiRestBasePath() { return r('apiRestBasePath', ''); },
  get charts() { return r('charts', ''); },
  get externalSearchApi() { return r('externalSearchApi', ''); },
  get searchApi() { return r('searchApi', ''); },
  get zambaWeb() { return r('zambaWeb', ''); },

  get cliente() { return r('cliente', 'zamba'); },

  api: {
    baseUrl: './',
    refreshTokenEnabled: true,
    refreshTokenType: 'auth-refresh',
  },
  modules: environmentModules,
} as Environment;
/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
