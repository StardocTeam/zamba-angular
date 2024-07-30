import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
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
  navigateUrl: SafeResourceUrl = '';
  result: boolean = false;

  DocType: string = '';
  docid: string = '';
  taskid: string = '';
  mode: string = '';
  s: string = '';
  userId: string = '';
  t: any = '';

  WebUrl = environment['zambaWeb'];

  needRefresh: boolean = true;

  /**
   *
   */
  constructor(
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService
  ) { }

  ngOnInit(): void {
    this.navigateUrl = '';
    this.result = false;

    this.cdr.detectChanges();

    this.route.queryParams.subscribe(params => {
      const tokenData = this.tokenService.get();

      if (tokenData != null) {
        this.DocType = params['DocType'];
        this.docid = params['docid'];
        this.taskid = params['taskid'];
        this.mode = params['mode'];
        this.s = params['s'];
        this.userId = params['userId'];

        this.userId = params['user'] || params['userid'] || params['u'];

        this.t = params['t'];

        var queryString =
          `?DocType=${this.DocType}&docid=${this.docid}&taskid=${this.taskid}&mode=${this.mode}&s=${this.s}&userId=${this.userId}&modalmode=true&` +
          `&t=${this.t}#Zamba`;

        var newUrl = `${this.WebUrl}/views/WF/TaskViewer${queryString}`;

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
  refreshContent() {
    this.needRefresh = false;
    this.cdr.detectChanges();
    this.needRefresh = true;
    this.cdr.detectChanges();
  }
}
