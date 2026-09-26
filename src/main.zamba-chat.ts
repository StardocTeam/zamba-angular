import { DoBootstrap, Injector, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { createCustomElement } from '@angular/elements';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { ZambaChatComponent } from './app/elements/zamba-chat/zamba-chat.component';

@NgModule({
  declarations: [ZambaChatComponent],
  imports: [BrowserModule, HttpClientModule, FormsModule],
})
class ZambaChatElementsModule implements DoBootstrap {
  constructor(private readonly injector: Injector) {}

  ngDoBootstrap(): void {
    if (!customElements.get('zamba-chat')) {
      customElements.define(
        'zamba-chat',
        createCustomElement(ZambaChatComponent, { injector: this.injector }),
      );
    }
  }
}

platformBrowserDynamic()
  .bootstrapModule(ZambaChatElementsModule)
  .catch((error: unknown) => console.error('[zamba-chat] bootstrap failed', error));
