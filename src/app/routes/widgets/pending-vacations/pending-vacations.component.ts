import { Component, EventEmitter, Inject, Input, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { GridsterItem } from 'angular-gridster2';
import { NzButtonSize } from 'ng-zorro-antd/button';
import { Subscription, catchError } from 'rxjs';

import { Generic } from './entitie/generic';
import { Vacation } from './entitie/vacation';
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
  @Input() divHeight: number = 600;
  result: boolean = false;
  loading: boolean = false;

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef,
    private PVService: PendingVacationsService,
    private router: Router
  ) { }

  ngOnInit(): void {
    if (this.resizeEvent != undefined && this.changeEvent != undefined) {
      this.resizeEvent.subscribe((event: any) => {

        if (this.widget['id'] == event.item.id) {
          var headerWidget = event.itemComponent.el.querySelector(".headerWidget").offsetHeight;
          var subHeaderWidget = event.itemComponent.el.querySelector(".subHeaderWidget").offsetHeight;

          this.divHeight = event.itemComponent.height - (headerWidget + subHeaderWidget);
          this.changeEvent.emit(event);
          this.cdr.detectChanges();
        }

      });

      this.changeEvent.subscribe((item: any) => { });
    }

    this.PostExternalsearchInfo();
  }
  PostExternalsearchInfo() {
    this.TotalDays = 0;
    this.vacations = [];
    this.info = false;
    this.result = false;
    this.loading = true;
    this.cdr.detectChanges();

    const tokenData: any = this.tokenService.get();
    let genericRequest = {};
    genericRequest = {
      UserId: 0,
      token: tokenData['token'],

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

          this.TotalDays = parseInt(JsonData['TotalDays'].toString() || "0");
        } else {
          this.info = false;
          this.result = true;
        }

        this.vacations.reverse();
        this.loading = false;
        this.cdr.detectChanges();
      });
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
