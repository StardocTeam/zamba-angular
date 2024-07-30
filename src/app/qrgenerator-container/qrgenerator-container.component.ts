import { Component, inject, ChangeDetectorRef, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { ModalHelper } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { catchError, finalize } from 'rxjs';

import { QRService } from './qr.service';
import { SignatureComponent } from '../signature/signature.component';
import { SignatureService } from '../signature/signature.service';

@Component({
  selector: 'app-qrgenerator-container',
  templateUrl: './qrgenerator-container.component.html',
  styleUrls: ['./qrgenerator-container.component.less']
})
export class QRGeneratorComponent implements OnInit {
  pdfUrl: SafeResourceUrl = this.sanitizer.bypassSecurityTrustResourceUrl('');
  private modalHelper = inject(ModalHelper);
  private msg = inject(NzMessageService);

  private pdffileid: string | any = '';
  public viewerMode = false;
  constructor(
    private sanitizer: DomSanitizer,
    private signatureService: SignatureService,
    private qrService: QRService,
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.pdffileid = this.route.snapshot.paramMap.get('id');

    if (this.pdffileid) {
      this.viewerMode = true;
      this.GetPDFBase64ByFileName();
    } else {
      this.getPDFBase64PayStub();
    }
  }

  GetPDFBase64ByFileName() {
    var genericRequest = {
      UserId: 0,
      token: 0,
      Params: {
        FileName: this.pdffileid
      }
    };
    this.qrService
      .GetPDFBase64ByFileName(genericRequest)
      .pipe(finalize(() => {}))
      .subscribe({
        next: result => {
          this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl(`data:application/pdf;base64,${result}`);
          this.cdr.detectChanges();
        },
        error: error => {
          this.msg.error('Error al cargar el documento');
        }
      });
  }

  getPDFBase64PayStub() {
    var genericRequest = {
      UserId: 0,
      token: 0,
      Params: {}
    };
    this.signatureService
      .GetPDFBase64PayStub(genericRequest)
      .pipe(finalize(() => {}))
      .subscribe({
        next: result => {
          this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl(`data:application/pdf;base64,${result}`);
          this.cdr.detectChanges();
        },
        error: error => {}
      });
  }

  SetQRCode(): void {
    var genericRequest = {
      UserId: 0,
      token: 0,
      Params: {}
    };
    this.qrService
      .GenerateQRCodePDF(genericRequest)
      .pipe(finalize(() => {}))
      .subscribe({
        next: result => {
          this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl(`data:application/pdf;base64,${result}`);
          this.cdr.detectChanges();
        },
        error: error => {}
      });
  }
}
