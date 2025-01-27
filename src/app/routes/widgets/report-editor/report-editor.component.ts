import { Component } from '@angular/core';
import { Report } from '../report-component/entitie/report';

@Component({
  selector: 'app-report-editor',
  templateUrl: './report-editor.component.html',
  styleUrls: ['./report-editor.component.less']
})
export class ReportEditorComponent {
  report: Report = {
    Name: '',
    Query: '',
    Aditional: 0,
    Completar: '',
    GroupExpression: '',
    RIGHT_TYPE: 0,
    USER_ID: 0,
    USER_NAME: '',
    OBJECTID: 0,
    ID: 0
  };
}
