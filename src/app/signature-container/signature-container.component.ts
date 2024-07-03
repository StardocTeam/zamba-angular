import { Component, inject, ChangeDetectorRef } from '@angular/core';

import { SignatureComponent } from '../signature/signature.component';

import { ModalHelper } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

import { SignatureService } from '../signature/signature.service';
import { catchError, finalize } from 'rxjs';

@Component({
  selector: 'app-signature-container',
  templateUrl: './signature-container.component.html',
  styleUrls: ['./signature-container.component.less']
})
export class SignatureContainerComponent {

  pdfUrl: SafeResourceUrl = this.sanitizer.bypassSecurityTrustResourceUrl('');
  private modalHelper = inject(ModalHelper);
  private msg = inject(NzMessageService);
  constructor(private sanitizer: DomSanitizer, private signatureService: SignatureService, private cdr: ChangeDetectorRef) {
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

  static(): void {
    this.modalHelper.createStatic(SignatureComponent, { record: { a: 1, b: '2', c: new Date() } }, { size: 'md' }).subscribe(res => {
      if (res != '') {
        this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl('data:application/pdf;base64,' + res);
        this.cdr.detectChanges();
      }
    });
  }
}
