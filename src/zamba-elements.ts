import { ChecklistComponent } from './app/elements/checklist/checklist.component';
import { ElementsModule } from './app/elements/elements-module';
import { createCustomElement } from '@angular/elements';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

// Bootstrap the module to obtain a proper injector and register the webcomponent.
platformBrowserDynamic()
  .bootstrapModule(ElementsModule)
  .then((moduleRef) => {
    const injector = moduleRef.injector;
    const ChecklistElement = createCustomElement(ChecklistComponent, { injector });
    if (!customElements.get('zamba-checklist')) {
      customElements.define('zamba-checklist', ChecklistElement);
    }
  })
  .catch((err) => console.error('[zamba-elements] bootstrap failed', err));

export { };
