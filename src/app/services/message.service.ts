import { Injectable, inject } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '@env/environment';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private router: Router) {
    this.handleMessage = this.handleMessage.bind(this);
  }
  startListening() {
    window.addEventListener('message', this.handleMessage,);
  }

  handleMessage(event: MessageEvent) {
    try {
      var url = new URL(environment['zambaWeb']);
      var origin = `${url.protocol}//${url.hostname}${url.port ? `:${url.port}` : ''}`;
      if (event.origin != origin) {
        return;
      }
      var message = JSON.parse(event.data);
      switch (message.type) {
        case 'navigate':
          console.log(message.data);
          this.router.navigate([message.data.data]);
          break;
      }
    } catch (e) {
      //console.error('Error al procesar el mensaje: ', e);
    }
  }

  sendMessage(type: any, data: any, error: any, elementId: string) {
    var url = new URL(environment['zambaWeb']);
    var origin = `${url.protocol}//${url.hostname}${url.port ? `:${url.port}` : ''}`;
    var message = {
      type: type,
      data: { data },
      error: error
    };
    var messageJSON = JSON.stringify(message);
    let iframeElement = document.getElementById(elementId) as HTMLIFrameElement;
    if (iframeElement && iframeElement.contentWindow) {
      iframeElement.contentWindow.postMessage(messageJSON, origin);
    }
  }
}
