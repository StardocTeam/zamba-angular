import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, Inject, Input, OnDestroy, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { GridsterItem } from 'angular-gridster2';
import { NzMessageService } from 'ng-zorro-antd/message';
import { BehaviorSubject, Observable, Subject, of, throwError } from 'rxjs';
import { catchError, finalize, takeUntil } from 'rxjs/operators';
import { ZambaService } from 'src/app/services/zamba/zamba.service';

import { PendingTasksService } from './service/pending-tasks.service';

interface ItemData {
  gender: string;
  name: Name;
  email: string;
}

interface Name {
  title: string;
  first: string;
  last: string;
}

@Component({
  selector: 'pending-tasks',
  templateUrl: './pending-tasks.component.html',
  styleUrls: ['pending-tasks.component.css']
})
export class PendingTasksComponent implements OnInit, OnDestroy {
  @Input()
  widget: any = {
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
  @Input() divHeight: number = 600;
  @Input() visualMode: string = 'dashboard';

  loading = false;
  data: any = [];
  private destroy$ = new Subject<boolean>();
  constructor(
    public nzMessage: NzMessageService,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private router: Router,
    private pendingTasksService: PendingTasksService,
    private cdr: ChangeDetectorRef,
    private http: HttpClient,
    private zambaService: ZambaService
  ) {}

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
  ngOnInit(): void {
    this.getMyTasks();
  }

  getMyTasks() {
    if (this.resizeEvent != undefined) {
      this.resizeEvent.subscribe((event: any) => {
        if (this.widget['id'] == event.item.id) {
          this.divHeight = event.itemComponent.height - 60;
          this.cdr.detectChanges();
        }
      });
    }

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
        .getMyTasks(genericRequest)
        .pipe(
          catchError(error => {
            console.error('Error en la solicitud:', error);
            return throwError(() => error);
          }),
          finalize(() => {
            this.loading = false;
            this.cdr.detectChanges();
          })
        )
        .subscribe(res => {
          try {
            this.data = res;
            this.cdr.detectChanges();
          } catch (error) {
            console.log('Resultado de buscar mis tareas:', res);
          }
        });
    }
  }

  OpenTask(url: string): void {
    const tokenData = this.tokenService.get();
    if (tokenData != null && tokenData['token'] != null) {
      try {
        window.open(`${url}&t=${tokenData['token']}`, '_blank');
      } catch (error) {
        console.log('Error al abrir la tarea: ', error);
      }
    }
  }

  GoToFormViewer(url: string) {
    //llamar al preflight para evitar que se desloguee
    this.zambaService.preFlightLogin();
    var token = this.tokenService.get();
    if (token != null) {
      const urlObject = new URL(`${url}&t=${token['token']}`);
      const params = urlObject.search;
      const urlToNavegate = `zamba/form${params}`;
      this.router.navigateByUrl(urlToNavegate);
    }
  }
}
