import { Component } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'change-password-expired',
  templateUrl: './change-password-expired.component.html',
})
export class ChangePasswordExpiredComponent {
  constructor(public msg: NzMessageService) {}
}
