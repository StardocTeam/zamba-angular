import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzTableModule } from 'ng-zorro-antd/table';
import { FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { PageHeaderModule } from '@delon/abc/page-header';
import { Router } from '@angular/router';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { TaskService } from 'src/app/services/task.service';
import { NzMessageModule, NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzSpinModule } from 'ng-zorro-antd/spin';


interface ItemData {
  [key: string]: any;
}
@Component({
  selector: 'app-do-show-table',
  standalone: true,
  imports: [CommonModule, NzTableModule, FormsModule, NzInputModule, PageHeaderModule, NzButtonModule, NzMessageModule, NzSpinModule],
  templateUrl: './test-signature.component.html',
  styleUrls: ['./test-signature.component.less']
})
export class TestSignatureComponent {

}
