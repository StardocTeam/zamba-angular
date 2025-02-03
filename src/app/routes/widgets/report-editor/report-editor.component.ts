import { Component } from '@angular/core';
import { Report } from '../report-component/entitie/report';

@Component({
  selector: 'app-report-editor',
  templateUrl: './report-editor.component.html',
  styleUrls: ['./report-editor.component.less']
})
export class ReportEditorComponent {
  report: Report = {
    Category: '',
    Description: '',
    Name: '',
    Query: '',
    Aditional: 0,
    Completar: '',
    ID: 0
  };

  onNgInit() {

    console.log('ReportEditorComponent onNgInit');
  }
}
