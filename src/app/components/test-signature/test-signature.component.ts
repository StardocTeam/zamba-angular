import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { PageHeaderModule } from '@delon/abc/page-header';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzMessageModule, NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzTableModule } from 'ng-zorro-antd/table';
import { TaskService } from 'src/app/services/task.service';

interface ItemData {
  [key: string]: any;
}
@Component({
  selector: 'app-do-show-table',
  standalone: true,
  imports: [CommonModule, NzTableModule, FormsModule, NzInputModule, PageHeaderModule, NzButtonModule, NzMessageModule, NzSpinModule],
  templateUrl: './test-signature.component.html',
  styleUrls: ['./test-signature.component.less'],
})
export class TestSignatureComponent {}
