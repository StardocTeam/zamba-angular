import { animate, state, style, transition, trigger } from '@angular/animations';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, ElementRef, HostListener, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { Subscription, of, throwError } from 'rxjs';
import { catchError, delay, finalize } from 'rxjs/operators';
import { ZambaService } from 'src/app/services/zamba/zamba.service';

import { PendingTasksService } from '../../service/pending-tasks.service';

@Component({
  selector: 'app-layout-pending-task-item',
  styleUrls: ['app-layout-pending-task-item.css'],
  templateUrl: './layout-pending-task-item.component.html'
})
export class LayoutPendingTaskItemComponent {
  nzType: string = 'unordered-list';
  routerPendingTasks: string = '/widgets/pending-tasks';
  direction: string = 'right';
  count: number = 0;
  PTDropDownOpened: boolean = false;
  timerSubscription: Subscription | undefined;

  constructor(
    private _eref: ElementRef,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private router: Router,
    private pendingTasksService: PendingTasksService,
    private cdr: ChangeDetectorRef,
    private zambaService: ZambaService
  ) {
    this.GetTaskCount();
  }

  @HostListener('document:click', ['$event'])
  clickout(event: any) {
    if (this.PTDropDownOpened && !this._eref.nativeElement.contains(event.target)) {
      this.PTDropDownOpened = false;
    }
  }

  GetTaskCount(): any {
    const tokenData = this.tokenService.get();

    if (tokenData != null && tokenData['userID'] != null) {
      const tokenData: any = this.tokenService.get();

      let genericRequest = {};
      genericRequest = {
        UserId: 0,
        token: tokenData['token'],
        Params: ''
      };

      this.pendingTasksService
        .getMyTasksCount(genericRequest)
        .pipe(
          catchError(error => {
            console.error('Error en la solicitud:', error);
            return throwError(() => error);
          }),
          finalize(() => {
            this.cdr.detectChanges();
          })
        )
        .subscribe(res => {
          try {
            this.count = res;
          } catch (error) {
            console.error('Error en la solicitud:', error);
          }
        });
    }
  }

  OpenPTDropDown() {
    this.PTDropDownOpened = !this.PTDropDownOpened;
    this.cdr.detectChanges();
  }

  timer() {
    this.timerSubscription = of(null)
      .pipe(delay(2000))
      .subscribe(() => (this.PTDropDownOpened = false));
  }

  keepOpened() {
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }
}
