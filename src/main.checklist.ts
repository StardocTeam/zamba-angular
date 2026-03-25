import { ElementsModule } from './app/elements/elements-module';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

// Bootstrap the module to obtain a proper injector and register the webcomponent.
platformBrowserDynamic()
  .bootstrapModule(ElementsModule)
  .catch((err) => console.error('[main.checklist] bootstrap failed', err));

export { };
