import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'change-password-result',
  templateUrl: './change-password-result.component.html',
})
export class ChangePasswordResultComponent {
  result = false;
  constructor(
    route: ActivatedRoute,
    public msg: NzMessageService,
  ) {
    let rv = route.snapshot.queryParams['rv'] || '';
    this.result = rv == 'ok';
  }
}
