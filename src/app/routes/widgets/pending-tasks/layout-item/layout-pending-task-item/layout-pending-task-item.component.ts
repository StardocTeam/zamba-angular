import { ChangeDetectorRef, Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { PendingTasksService } from '../../service/pending-tasks.service';
import { HttpClient } from '@angular/common/http';
import { ZambaService } from 'src/app/services/zamba/zamba.service';
import { catchError, finalize } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { LayoutDefaultOptions } from '@delon/theme/layout-default';

@Component({
  selector: 'app-layout-pending-task-item',
  templateUrl: './layout-pending-task-item.component.html'
})
export class LayoutPendingTaskItemComponent {
  nzType: string = 'unordered-list';
  routerPendingTasks: string = '/widgets/pending-tasks';
  direction: string = 'right';
  count: number = 0;

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private router: Router,
    private pendingTasksService: PendingTasksService,
    private cdr: ChangeDetectorRef,
    private http: HttpClient,
    private zambaService: ZambaService
  ) {
    this.GetTaskCount();
    //this.cdr.detectChanges();
  }

  GetTaskCount(): any {
    const tokenData = this.tokenService.get();
    console.log('Token data:', tokenData);
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
            console.log('Resultado de buscar mis tareas:', res);
          }
        });
    }
  }

}
