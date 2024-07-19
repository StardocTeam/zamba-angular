import { Component, inject, ChangeDetectorRef, OnInit, Input } from '@angular/core';

import { ModalHelper } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

import { SignatureService } from '../signature/signature.service';
import { catchError, finalize } from 'rxjs';
import { ZambaService } from '../services/zamba/zamba.service';
import { SignatureV2Component } from '../signature-v2/signature-v2.component';

@Component({
  selector: 'signature-fab',
  templateUrl: './signature-container-v2.component.html',
  styleUrls: ['./signature-container-v2.component.less']
})
export class SignatureFABComponent implements OnInit {

  @Input()
  docType: any;
  @Input()
  docId: any;

  showFABButton: boolean = false;
  pdfUrl: SafeResourceUrl = this.sanitizer.bypassSecurityTrustResourceUrl('');
  private modalHelper = inject(ModalHelper);
  private msg = inject(NzMessageService);
  constructor(private sanitizer: DomSanitizer, private zambaService: ZambaService, private signatureService: SignatureService, private cdr: ChangeDetectorRef) {
    this.zambaService.preFlightLogin();
    this.TaskViewerMessageHandler = this.TaskViewerMessageHandler.bind(this);
  }
  ngOnInit(): void {
    window.addEventListener('message', this.TaskViewerMessageHandler);

  }
  ValidateAlreadySigned() {
    var genericRequest = {
      UserId: 0,
      token: 0,
      Params: {
        DocType: this.docType,
        DocId: this.docId
      }
    };
    this.signatureService.ValidateAlreadySigned(genericRequest).pipe(
      finalize(() => {
      }),
    ).subscribe({
      next: (result) => {
        this.showFABButton = !result.indexAlreadySigned;
      },
      error: (error) => {
      }
    });
  }


  static(): void {
    this.modalHelper.createStatic(SignatureV2Component, { record: { a: 1, b: '2', c: new Date() } }, { size: 'lg' }).subscribe(res => {
      if (res != '') {
        this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl('data:application/pdf;base64,' + res);
        this.cdr.detectChanges();
      }
    });
  }

  TaskViewerMessageHandler(event: MessageEvent) {
    try {
      var message = JSON.parse(event.data);

      switch (message.type) {
        case 'signature':
          this.ValidateAlreadySigned();
          break;
      }
    } catch (error) { }
  }
}
