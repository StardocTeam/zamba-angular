import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'reset-result',
  templateUrl: './reset-result.component.html',
})
export class ResetResultComponent {
  params = { email: '' };
  email = '';
  constructor(
    route: ActivatedRoute,
    public msg: NzMessageService,
  ) {
    this.params.email = this.email = route.snapshot.queryParams['email'] || 'ng-alain@example.com';
  }
}
