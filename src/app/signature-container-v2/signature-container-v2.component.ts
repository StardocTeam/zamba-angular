import { Component, inject, ChangeDetectorRef, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ModalHelper } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { catchError, finalize } from 'rxjs';

import { ZambaService } from '../services/zamba/zamba.service';
import { SignatureService } from '../signature/signature.service';
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

  @Output() refreshRequested = new EventEmitter<void>();

  showFABButton: boolean = false;
  pdfUrl: SafeResourceUrl = this.sanitizer.bypassSecurityTrustResourceUrl('');
  private modalHelper = inject(ModalHelper);
  private msg = inject(NzMessageService);
  constructor(
    private sanitizer: DomSanitizer,
    private zambaService: ZambaService,
    private signatureService: SignatureService,
    private cdr: ChangeDetectorRef
  ) {
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
    this.signatureService
      .ValidateAlreadySigned(genericRequest)
      .pipe(finalize(() => {}))
      .subscribe({
        next: result => {
          var objectResult = JSON.parse(result);
          this.showFABButton = !objectResult.indexAlreadySigned;
        },
        error: error => {}
      });
  }

  static(): void {
    this.modalHelper
      .createStatic(SignatureV2Component, { record: { docType: this.docType, docId: this.docId } }, { size: 'lg' })
      .subscribe(res => {
        if (res != '') {
          this.cdr.detectChanges();
          this.requestRefresh();
        }
      });
  }

  requestRefresh() {
    this.refreshRequested.emit();
  }
  TaskViewerMessageHandler(event: MessageEvent) {
    try {
      var message = JSON.parse(event.data);

      switch (message.type) {
        case 'signature':
          this.ValidateAlreadySigned();
          break;
      }
    } catch (error) {}
  }
}
