import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { environment } from 'environments/environment';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class Globals {
  //TO DO INJECTABLE...O GLOBAL A LOS COMPONENTES
  private _port: string = "";
  // ApiUrl: string = "http://localhost" + this.port() + "/zambaweb.restapi/api/";
  ApiUrl: string = environment.url;
  
    private port() {
      if (this._port == "") {
        return "";
      } else {
        return this._port;
      }
    }
 }

