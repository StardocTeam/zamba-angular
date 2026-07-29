import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import {
  AfterViewInit,
  Component,
  inject,
  OnInit,
  ViewChild,
  ChangeDetectionStrategy,
  ViewEncapsulation,
  Renderer2,
  ChangeDetectorRef,
  Inject,
  OnDestroy,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { ITokenService, DA_SERVICE_TOKEN } from '@delon/auth';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { catchError, of, tap } from 'rxjs';

import { SpanishPaginatorIntl } from './spanish-paginator';
import { TaskHistoryService } from '../../services/task-history-service.service';

type HistoryMode = 'taskHistory' | 'indexesHistory' | 'generalHistory';

interface HistoryResponse {
  data: any[];
  columnNames: string[];
  totalRecords?: number;
}

interface HistoryCache {
  columnNames: string[];
  pages: Map<number, any[]>;
  pageSize: number;
  totalRecords: number;
}

@Component({
  selector: 'app-task-history',
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginatorModule,
    HttpClientModule,
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatDividerModule,
    NzSpinModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    NzBreadCrumbModule,
  ],
  templateUrl: './task-history.component.html',
  styleUrls: ['./task-history.component.css'],
  providers: [TaskHistoryService, { provide: MatPaginatorIntl, useClass: SpanishPaginatorIntl }],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.Emulated,
})
export class TaskHistoryComponent implements AfterViewInit, OnDestroy, OnInit {
  @ViewChild(MatSort) sort: MatSort | undefined;
  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  private taskHistoryService = inject(TaskHistoryService);
  private route = inject(ActivatedRoute);
  isLoading: boolean = false; // Estado de carga
  showNoDataMessage: boolean = false;

  lastSelectedButton: HistoryMode | null = null;
  displayedColumns: string[] = [];
  dataSource = new MatTableDataSource<any>();
  docId: number = 0;
  taskId: number = 0;
  taskName: string = '';
  readonly pageSizeOptions = [10, 20];
  readonly defaultPageSize = 10;
  currentPageIndex = 0;
  pageSize = this.defaultPageSize;
  totalRecords = 0;

  private fontLink: HTMLLinkElement | null = null;
  private readonly historyCaches: Record<HistoryMode, HistoryCache> = {
    taskHistory: this.createEmptyCache(),
    indexesHistory: this.createEmptyCache(),
    generalHistory: this.createEmptyCache(),
  };

  constructor(
    private renderer: Renderer2,
    private cd: ChangeDetectorRef,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
  ) {}

  ngAfterViewInit() {
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
      this.clearCaches();
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
    this.taskHistoryService
      .getTaskName(this.docId, this.taskId)
      .pipe(
        tap(response => {
          this.taskName = response;
          this.cd.detectChanges();
        }),
        catchError(error => {
          console.error('Error fetching task name:', error);
          return of([]);
        }),
      )
      .subscribe();
  }
  taskHistoryOnClick(): void {
    this.selectHistory('taskHistory');
  }

  indexesHistoryOnClick(): void {
    this.selectHistory('indexesHistory');
  }

  generalHistoryOnClick(): void {
    this.selectHistory('generalHistory');
  }

  refreshHistoryData(): void {
    if (!this.lastSelectedButton) {
      this.lastSelectedButton = 'taskHistory';
    }

    this.currentPageIndex = 0;
    this.clearCaches();

    if (this.paginator) {
      this.paginator.pageIndex = 0;
      this.paginator.pageSize = this.pageSize;
    }

    this.loadCurrentPage();
  }

  onPageChange(event: PageEvent): void {
    if (!this.lastSelectedButton) {
      return;
    }

    if (event.pageSize !== this.pageSize) {
      this.pageSize = event.pageSize;
      this.currentPageIndex = 0;
      this.resetCache(this.lastSelectedButton);

      if (this.paginator) {
        this.paginator.pageIndex = 0;
      }
    } else {
      this.currentPageIndex = event.pageIndex;
    }

    this.loadCurrentPage();
  }

  private selectHistory(mode: HistoryMode): void {
    this.lastSelectedButton = mode;
    this.currentPageIndex = 0;

    if (this.paginator) {
      this.paginator.pageIndex = 0;
      this.paginator.pageSize = this.pageSize;
    }

    this.loadCurrentPage();
  }

  private loadCurrentPage(): void {
    if (!this.lastSelectedButton) {
      return;
    }

    const cache = this.historyCaches[this.lastSelectedButton];

    if (cache.pageSize === this.pageSize && cache.pages.has(this.currentPageIndex)) {
      this.displayCachedPage(cache);
      return;
    }

    this.isLoading = true; // Inicia el spinner
    this.showNoDataMessage = false;
    this.dataSource.data = [];

    const mode = this.lastSelectedButton;
    const pageIndex = this.currentPageIndex;
    const pageSize = this.pageSize;
    let request = this.taskHistoryService.getGeneralHistoryPaged(this.docId, pageIndex, pageSize);

    if (mode === 'taskHistory') {
      request = this.taskHistoryService.getTaskHistoryPaged(this.docId, this.taskId, pageIndex, pageSize);
    } else if (mode === 'indexesHistory') {
      request = this.taskHistoryService.getIndexesHistoryPaged(this.docId, pageIndex, pageSize);
    }

    request
      .pipe(
        tap(response => {
          const isCurrentRequest = this.lastSelectedButton === mode && this.currentPageIndex === pageIndex && this.pageSize === pageSize;

          if (response !== undefined) {
            const parsedResponse = this.parseHistoryResponse(response);
            const activeCache = this.historyCaches[mode];
            const totalRecords = parsedResponse.totalRecords ?? parsedResponse.data.length;
            const pageData = this.getVisiblePageData(parsedResponse.data, totalRecords, pageIndex, pageSize);

            activeCache.columnNames = parsedResponse.columnNames;
            activeCache.pageSize = pageSize;
            activeCache.totalRecords = totalRecords;
            activeCache.pages.set(pageIndex, pageData);

            if (isCurrentRequest) {
              this.displayCachedPage(activeCache);
            }
          } else {
            if (!isCurrentRequest) {
              return;
            }
            this.showNoDataMessage = true;
          }
          if (!isCurrentRequest) {
            return;
          }
          this.isLoading = false; // Detiene el spinner
          this.cd.detectChanges();
        }),
        catchError(error => {
          console.error('Error fetching history page:', error);
          this.isLoading = false; // Detiene el spinner
          this.showNoDataMessage = true;
          this.cd.detectChanges();
          return of([]);
        }),
      )
      .subscribe();
  }

  private displayCachedPage(cache: HistoryCache): void {
    this.displayedColumns = cache.columnNames;
    this.totalRecords = cache.totalRecords;
    this.dataSource.data = cache.pages.get(this.currentPageIndex) ?? [];
    this.showNoDataMessage = cache.totalRecords === 0;
    this.isLoading = false;
    this.cd.detectChanges();
  }

  private parseHistoryResponse(response: any): HistoryResponse {
    return typeof response === 'string' ? JSON.parse(response) : response;
  }

  private getVisiblePageData(data: any[], totalRecords: number, pageIndex: number, pageSize: number): any[] {
    const firstRecordIndex = pageIndex * pageSize;
    const remainingRecords = Math.max(totalRecords - firstRecordIndex, 0);
    const visibleRecords = Math.min(pageSize, remainingRecords);

    return data.slice(0, visibleRecords);
  }

  private resetCache(mode: HistoryMode): void {
    this.historyCaches[mode] = this.createEmptyCache();
    this.displayedColumns = [];
    this.dataSource.data = [];
    this.totalRecords = 0;
    this.showNoDataMessage = false;
  }

  private clearCaches(): void {
    this.resetCache('taskHistory');
    this.resetCache('indexesHistory');
    this.resetCache('generalHistory');
  }

  private createEmptyCache(): HistoryCache {
    return {
      columnNames: [],
      pages: new Map<number, any[]>(),
      pageSize: this.defaultPageSize,
      totalRecords: 0,
    };
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
