import { Component, inject, ChangeDetectorRef } from '@angular/core';

import { SignatureComponent } from '../signature/signature.component';

import { ModalHelper } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

import { SignatureService } from '../signature/signature.service';
import { catchError, finalize } from 'rxjs';
import { QRService } from './qr.service';

@Component({
  selector: 'app-qrgenerator-container',
  templateUrl: './qrgenerator-container.component.html',
  styleUrls: ['./qrgenerator-container.component.less']
})
export class QRGeneratorComponent {

  pdfUrl: SafeResourceUrl = this.sanitizer.bypassSecurityTrustResourceUrl('');
  private modalHelper = inject(ModalHelper);
  private msg = inject(NzMessageService);
  constructor(private sanitizer: DomSanitizer, private signatureService: SignatureService, private qrService: QRService, private cdr: ChangeDetectorRef) {
    this.getPDFBase64PayStub();
  }

  getPDFBase64PayStub() {
    var genericRequest = {
      UserId: 0,
      token: 0,
      Params: {}
    };
    this.signatureService.GetPDFBase64PayStub(genericRequest).pipe(
      finalize(() => {
      }),
    ).subscribe({
      next: (result) => {
        this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl('data:application/pdf;base64,' + result);
        this.cdr.detectChanges();
      },
      error: (error) => {
      }
    });
  }
  open(): void {
    console.log('click');
    this.modalHelper.create(SignatureComponent, { record: { a: 1, b: '2', c: new Date() } }, { size: 'md' }).subscribe(res => {

    });
  }

  SetQRCode(): void {
    var genericRequest = {
      UserId: 0,
      token: 0,
      Params: {}
    };
    this.qrService.GenerateQRCodePDF(genericRequest).pipe(
      finalize(() => {
      }),
    ).subscribe({
      next: (result) => {
        this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl('data:application/pdf;base64,' + result);
        this.cdr.detectChanges();
      },
      error: (error) => {
      }
    });
  }
}
