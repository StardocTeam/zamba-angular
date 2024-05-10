import { Component, Inject, OnInit } from '@angular/core';
import { ChangeDetectorRef } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { ITokenService, DA_SERVICE_TOKEN } from '@delon/auth';
import { environment } from '../../../../environments/environment';



@Component({
  selector: 'app-view-form',
  templateUrl: './view-form.component.html',
  styleUrls: ['./view-form.component.less']
})
export class ViewFormComponent implements OnInit {
  navigateUrl: SafeResourceUrl = "";
  result: boolean = false;


  DocType: string = '';
  docid: string = '';
  taskid: string = '';
  mode: string = '';
  s: string = '';
  userId: string = '';
  t: any = "";

  WebUrl = environment['zambaWeb'];

  /**
   *
   */
  constructor(private cdr: ChangeDetectorRef, private route: ActivatedRoute, private sanitizer: DomSanitizer, @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService) {

  }


  ngOnInit(): void {
    this.navigateUrl = '';
    this.result = false;
    this.cdr.detectChanges();

    this.route.queryParams.subscribe(params => {
      const tokenData = this.tokenService.get();

      if (tokenData != null) {
        console.log('Imprimo los valores en tokenService en el service', tokenData);
        debugger;
        //TODO: validar valores de tokenData
        this.DocType = params["DocType"];
        this.docid = params["docid"];
        this.taskid = params["taskid"];
        this.mode = params["mode"];
        this.s = params["s"];
        this.userId = params["userId"];
        this.t = params["t"];

        var queryString = "?DocType=" + this.DocType + "&docid=" + this.docid + "&taskid=" + this.taskid + "&mode=" + this.mode + "&s=" + this.s + "&userId=" + this.userId + "&modalmode=true&" + "&t=" + this.t;

        var newUrl = this.WebUrl + "/views/WF/TaskViewer" + queryString;

        // Encode string to Base64
        const encodedString = this.encodeStringToBase64(JSON.stringify(tokenData));
        this.navigateUrl = this.sanitizer.bypassSecurityTrustResourceUrl(newUrl);
        this.result = true;
        this.cdr.detectChanges();
      }
    });
  }


  encodeStringToBase64(str: string): string {
    return btoa(str);
  }

  // Function to decode base64 to string
  decodeBase64ToString(base64: string): string {
    return atob(base64);
  }

}
