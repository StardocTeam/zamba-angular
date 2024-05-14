import { Component, EventEmitter, Inject, Input, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { GridsterItem } from 'angular-gridster2';
import { NzButtonSize } from 'ng-zorro-antd/button';
import { Subscription, catchError } from 'rxjs';

import { Vacation } from './entitie/vacation';
import { Generic } from "./entitie/generic";

import { PendingVacationsService } from './service/pending-vacations.service';

@Component({
  selector: 'app-pending-vacations',
  templateUrl: './pending-vacations.component.html',
  styleUrls: ['./pending-vacations.component.less']
})
export class PendingVacationsComponent implements OnInit {
  vacations: Vacation[] = [];
  TotalDays: number = 0;
  size_APPROVED_VACATIONS: NzButtonSize = 'small';
  size_DAYS_AVAILABLE: NzButtonSize = 'small';
  info: boolean = true;

  @Input()
  widget: GridsterItem = {
    type: '',
    title: '',
    cols: 0,
    rows: 0,
    x: 0,
    y: 0,
    resizeEvent: new EventEmitter<GridsterItem>()
  };

  @Input()
  resizeEvent: EventEmitter<GridsterItem> = new EventEmitter<GridsterItem>();

  @Input()
  changeEvent: EventEmitter<GridsterItem> = new EventEmitter<GridsterItem>();

  private resizeSubscription: Subscription | undefined;
  private changeSubscription: Subscription | undefined;
  result: boolean = false;
  loading: boolean = false;

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef,
    private PVService: PendingVacationsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.GetExternalsearchInfo();
    //this.PostExternalsearchInfo();

    this.resizeSubscription = this.resizeEvent.subscribe((event: any) => {
      // console.log("🔴: " + event);
      if (this.widget['name'] == event.item['name']) {
        this.changeEvent.emit(event);
        this.cdr.detectChanges();
      }
    });
    this.changeSubscription = this.changeEvent.subscribe((item: any) => {
      // console.log("🟢: " + item);
    });
  }
  GetExternalsearchInfo() {
    this.vacations = [];
    this.info = false;
    this.result = false;
    this.loading = true;
    this.cdr.detectChanges();

    const tokenData = this.tokenService.get();
    let request: Generic = new Generic();

    if (tokenData != null) {
      request = {
        UserId: tokenData['userid'],
        EntityID: '258',
        DoctypesId: '110'
      };

      this.PVService._GetExternalsearchInfo(request)
        .pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            this.loading = false;
            this.cdr.detectChanges();
            throw error;
          })
        )
        .subscribe(data => {
          var JsonData = JSON.parse(data);

          if (JsonData != null) {
            for (let item of JsonData.VacationList) {
              var vacationItem: Vacation = new Vacation();

              vacationItem.AuthorizeOption = item['AuthorizeOption'];
              vacationItem.RequestedDaysOption = item['RequestedDaysOption'];
              vacationItem.VacationFromOption = item['VacationFromOption'];
              vacationItem.VacationToOption = item['VacationToOption'];

              vacationItem.DocType = item['DocType'];
              vacationItem.docid = item['docid'];
              vacationItem.taskid = item['taskid'];
              vacationItem.mode = item['mode'];
              vacationItem.s = item['s'];
              vacationItem.userId = item['userId'];

              this.vacations.push(vacationItem);
              this.info = true;
              this.result = true;
            }

            this.TotalDays = parseInt(JsonData['TotalDays'].toString());

          } else {
            this.info = false;
            this.result = true;

          }

          this.vacations.reverse();
          this.loading = false;
          this.cdr.detectChanges();
        });
    }
  }

  PostExternalsearchInfo() {
    this.vacations = [];
    this.info = false;
    this.result = false;
    this.loading = true;
    this.cdr.detectChanges();

    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userID'],
        Params: {
          EntityID: '258',
          DoctypesId: '110'
        }
      };

      this.PVService._PostExternalsearchInfo(genericRequest)
        .pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            this.loading = false;
            this.cdr.detectChanges();
            throw error;
          })
        )
        .subscribe(data => {
          var JsonData = JSON.parse(data);

          if (JsonData != null) {
            for (let item of JsonData.VacationList) {
              var vacationItem: Vacation = new Vacation();

              vacationItem.AuthorizeOption = item['AuthorizeOption'];
              vacationItem.RequestedDaysOption = item['RequestedDaysOption'];
              vacationItem.VacationFromOption = item['VacationFromOption'];
              vacationItem.VacationToOption = item['VacationToOption'];

              vacationItem.DocType = item['DocType'];
              vacationItem.docid = item['docid'];
              vacationItem.taskid = item['taskid'];
              vacationItem.mode = item['mode'];
              vacationItem.s = item['s'];
              vacationItem.userId = item['userId'];

              this.vacations.push(vacationItem);
              this.info = true;
              this.result = true;
            }

            this.TotalDays = parseInt(JsonData['TotalDays'].toString());
          } else {
            this.info = false;
            this.result = true;
          }

          this.vacations.reverse();
          this.loading = false;
          this.cdr.detectChanges();
        });
    }
  }

  GoToTask(obj: any) {
    var token = this.tokenService.get();
    if (token != null) {

      this.router.navigate(['/zamba/form'], {
        queryParams: {
          DocType: obj.DocType,
          docid: obj.docid,
          taskid: obj.taskid,
          mode: obj.mode,
          s: obj.s,
          userId: obj.userId,
          modalmode: 'true',
          t: token['token']
        }
      });
    }
  }

  RequestVacation() {
    var route = '/zamba/rule';
    this.router.navigate([route], { queryParams: { typeRule: 'executeViewTask', ruleId: '133' } });
  }
}
