import { AfterViewInit, Component, inject, OnInit, ViewChild, ChangeDetectionStrategy, ViewEncapsulation, Renderer2, ChangeDetectorRef, Inject } from '@angular/core';
import { MatPaginator, MatPaginatorModule, MatPaginatorIntl } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { TaskHistoryService } from '../../services/task-history-service.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { catchError, of, tap } from 'rxjs';
import { SpanishPaginatorIntl } from './spanish-paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { ActivatedRoute, Router } from '@angular/router';
import { ITokenService, DA_SERVICE_TOKEN } from '@delon/auth';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';

@Component({
  selector: 'app-task-history',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, HttpClientModule, CommonModule, MatCardModule, MatButtonModule, MatIconModule,
    MatDividerModule, NzSpinModule, MatSortModule, MatFormFieldModule, MatInputModule, NzBreadCrumbModule],
  templateUrl: './task-history.component.html',
  styleUrls: ['./task-history.component.css'],
  providers: [TaskHistoryService, { provide: MatPaginatorIntl, useClass: SpanishPaginatorIntl }],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.Emulated
})
export class TaskHistoryComponent implements AfterViewInit, OnInit {
  @ViewChild(MatSort) sort: MatSort | undefined;
  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  private taskHistoryService = inject(TaskHistoryService);
  private route = inject(ActivatedRoute);
  isLoading: boolean = false; // Estado de carga
  showNoDataMessage: boolean = false;

  lastSelectedButton: string | null = null;
  displayedColumns: string[] = [];
  dataSource = new MatTableDataSource<any>();
  docId: number = 0;
  taskId: number = 0;
  taskName: string = '';

  private fontLink: HTMLLinkElement | null = null;


  constructor(
    private renderer: Renderer2,
    private cd: ChangeDetectorRef,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private router: Router) {

  }

  ngAfterViewInit() {
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
    }
    if (this.sort) {
      this.dataSource.sort = this.sort;
    }
  }

  ngOnDestroy() {
    if (this.fontLink) {
      this.renderer.removeChild(document.head, this.fontLink);
    }
  }


  ngOnInit(): void {

    this.route.queryParamMap.subscribe(params => {
      const docIdParam = params.get('docid');
      const taskIdParam = params.get('taskid');
      const tokenParam = params.get('t');
      if (docIdParam) {
        this.docId = +docIdParam;
      }
      if (taskIdParam) {
        this.taskId = +taskIdParam;
      }
      if (tokenParam) {
        this.tokenService.set({ token: tokenParam });
      }
      this.taskHistoryOnClick();
      this.getTaskName();

    });




    this.fontLink = this.renderer.createElement('link');
    if (this.fontLink) {
      this.fontLink.rel = 'stylesheet';
      this.fontLink.href = 'https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500&display=swap';
      this.renderer.appendChild(document.head, this.fontLink);
    }
  }

  getTaskName(): void {
    this.taskHistoryService.getTaskName(this.docId, this.taskId)
      .pipe(
        tap(response => {
          this.taskName = response;
          this.cd.detectChanges();
        }),
        catchError(error => {
          console.error('Error fetching task name:', error);
          return of([]);
        })
      ).subscribe();
  }
  taskHistoryOnClick(): void {
    this.isLoading = true; // Inicia el spinner
    this.showNoDataMessage = false;

    this.displayedColumns = [];

    this.lastSelectedButton = 'taskHistory';

    this.taskHistoryService.getTaskHistory(this.docId, this.taskId)
      .pipe(
        tap(response => {
          if (response != undefined) {
            response = JSON.parse(response);
            this.dataSource.data = response.data;
            this.displayedColumns = response.columnNames;
            if (this.paginator) {
              this.dataSource.paginator = this.paginator;
            }
            this.showNoDataMessage = response.data.length === 0;
          } else {
            this.showNoDataMessage = true;
          }
          this.isLoading = false; // Detiene el spinner
          this.cd.detectChanges();
        }),
        catchError(error => {
          console.error('Error fetching task history:', error);
          this.isLoading = false; // Detiene el spinner
          this.cd.detectChanges();
          return of([]);
        })
      ).subscribe();
  }

  indexesHistoryOnClick(): void {
    this.isLoading = true; // Inicia el spinner
    this.showNoDataMessage = false;

    this.displayedColumns = [];

    this.lastSelectedButton = 'indexesHistory';

    this.taskHistoryService.getIndexesHistory(this.docId)
      .pipe(
        tap(response => {
          console.log(response);
          if (response != undefined) {
            response = JSON.parse(response);
            this.dataSource.data = response.data;
            this.displayedColumns = response.columnNames;
            this.showNoDataMessage = response.data.length === 0;
          } else {
            this.showNoDataMessage = true;
          }
          this.isLoading = false; // Detiene el spinner
          this.cd.detectChanges();
        }),
        catchError(error => {
          console.error('Error fetching indexes history:', error);
          this.isLoading = false; // Detiene el spinner
          this.cd.detectChanges();
          return of([]);
        }),
      ).subscribe();
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
