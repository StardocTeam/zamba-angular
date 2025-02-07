import { Component } from '@angular/core';
import { Report } from '../report-component/entitie/report';
import { FormsModule } from '@angular/forms';

import { NzInputModule } from 'ng-zorro-antd/input';

@Component({
  selector: 'app-report-editor',
  templateUrl: './report-editor.component.html',
  styleUrls: ['./report-editor.component.less']
})
export class ReportEditorComponent {
  myGroup: any;
  inputValue?: string = "";
  report: Report = {
    Category: '',
    Description: '',
    Name: '',
    Query: '',
    Aditional: 0,
    Completar: '',
    ID: 0
  };

  constructor() {
    console.log('ReportEditorComponent constructor');

  }

  onNgInit() {
    console.log('ReportEditorComponent onNgInit');
  }

  onNgDestroy() {
    console.log('ReportEditorComponent onNgDestroy');
  }

  InsertReport() {
    throw new Error('Method not implemented.');
  }
}
