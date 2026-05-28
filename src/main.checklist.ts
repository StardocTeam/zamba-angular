import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { ElementsModule } from './app/elements/elements-module';

// Bootstrap the module to obtain a proper injector and register the webcomponent.
platformBrowserDynamic()
  .bootstrapModule(ElementsModule)
  .catch(err => console.error('[main.checklist] bootstrap failed', err));

export {};
