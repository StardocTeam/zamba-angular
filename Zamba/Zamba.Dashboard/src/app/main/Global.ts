
import { environment } from 'environments/environment';
export class Globals {    
    private _port : string;
    public get port() : string {
        return this._port;
    }
    public set port(v : string) {
        this._port = v;
    }
    
    private _ApiUrl : string;
    public get ApiUrl() : string {
        return this._ApiUrl;
    }
    public set value(v : string) {
        this._ApiUrl = v;
    }    



    constructor() {
         this._port = "44301";
        // this._ApiUrl = "http://localhost"+this.setPort()+"/zambaweb.restapi/api/";

        this._ApiUrl = environment.urlApi;
    }


    
    setPort():string{
        if (this._port == "") {
            return "";
        } else {
            return ":" + this._port;            
        }     
    }
}