import { Component, EventEmitter, Inject, Input, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { GridsterItem } from 'angular-gridster2';
import { NzButtonSize } from 'ng-zorro-antd/button';
import { Subscription, catchError } from 'rxjs';

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
  size: NzButtonSize = 'large';
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

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef,
    private PVService: PendingVacationsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.GetExternalsearchInfo();

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
    this.info = false;
    this.result = false;
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

      this.PVService._GetExternalsearchInfo(genericRequest)
        .pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            throw error;
          })
        )
        .subscribe(data => {
          var JsonData = JSON.parse(data);

          if (JsonData != null) {
            for (let item of JsonData) {
              var vacationItem: Vacation = new Vacation();

              vacationItem.AuthorizeOption = item['AuthorizeOption'];
              vacationItem.RequestedDaysOption = item['RequestedDaysOption'];
              vacationItem.VacationFromOption = item['VacationFromOption'];
              vacationItem.VacationToOption = item['VacationToOption'];

              //TODO: Refactorizar si es que cambia la estructura de forma definitiva.
              this.TotalDays = JsonData[JsonData.length - 1]['TotalDays'].toString();
              this.vacations.push(vacationItem);
              this.info = true;
              this.result = true;
            }
          } else {
            this.info = false;
            this.result = true;
          }

          this.cdr.detectChanges();
        });
    }
  }

  RequestVacation() {
    var route = '/zamba/rule';
    this.router.navigate([route], { queryParams: { typeRule: 'executeViewTask', ruleId: '133' } });
  }
}
