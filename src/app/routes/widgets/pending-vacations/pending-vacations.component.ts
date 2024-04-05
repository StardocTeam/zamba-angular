import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { NzButtonSize } from 'ng-zorro-antd/button';
import { NZ_I18N, en_US } from 'ng-zorro-antd/i18n';
import { NZ_ICONS } from 'ng-zorro-antd/icon';
import { catchError } from 'rxjs';

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

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef,
    private PVService: PendingVacationsService
  ) {}

  ngOnInit(): void {
    this.GetExternalsearchInfo();
    this.GetVacation();
  }

  private GetVacation() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        Params: ''
      };

      this.PVService._GetVacation(genericRequest)
        .pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            throw error; // Puedes relanzar el error o retornar un valor por defecto
          })
        )
        .subscribe(data => {});
    }
  }

  submit() {}
  GetExternalsearchInfo() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
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

          if (this.vacations != null) {
            for (let item of JsonData) {
              var vacationItem: Vacation = new Vacation();

              vacationItem.AuthorizeOption = item['AuthorizeOption'];
              vacationItem.RequestedDaysOption = item['RequestedDaysOption'];
              vacationItem.VacationFromOption = item['VacationFromOption'];
              vacationItem.VacationToOption = item['VacationToOption'];

              this.TotalDays = item['TotalDays'].toString();
              this.vacations.push(vacationItem);
            }
          }

          this.cdr.detectChanges();
        });
    }
  }
}
