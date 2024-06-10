import { Component, inject } from '@angular/core';

import { SignatureComponent } from '../signature/signature.component';

import { ModalHelper } from '@delon/theme';
import { NzMessageService } from 'ng-zorro-antd/message';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-signature-container',
  templateUrl: './signature-container.component.html',
  styleUrls: ['./signature-container.component.less']
})
export class SignatureContainerComponent {

  pdfUrl: SafeResourceUrl;;
  private modalHelper = inject(ModalHelper);
  private msg = inject(NzMessageService);
  constructor(private sanitizer: DomSanitizer,) {
    this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl("https://pdfobject.com/pdf/sample.pdf");
  }

  open(): void {
    console.log('click');
    this.modalHelper.create(SignatureComponent, { record: { a: 1, b: '2', c: new Date() } }, { size: 'md' }).subscribe(res => {
      this.msg.info(res);
    });
  }

  static(): void {
    this.modalHelper.createStatic(SignatureComponent, { record: { a: 1, b: '2', c: new Date() } }, { size: 'md' }).subscribe(res => {
      this.msg.info(res);
    });
  }


}
