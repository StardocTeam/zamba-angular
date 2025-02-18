import { AfterViewInit, Component, inject, OnInit, ViewChild, ChangeDetectionStrategy, ViewEncapsulation, Renderer2 } from '@angular/core';
import { MatPaginator, MatPaginatorModule, MatPaginatorIntl } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { TaskHistoryService } from '../../services/task-history-service.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { catchError, of, tap } from 'rxjs';
import { SpanishPaginatorIntl } from './spanish-paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { ActivatedRoute, } from '@angular/router';

@Component({
  selector: 'app-task-history',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, HttpClientModule, CommonModule, MatCardModule, MatButtonModule],
  templateUrl: './task-history.component.html',
  styleUrls: ['./task-history.component.css'],
  providers: [TaskHistoryService, { provide: MatPaginatorIntl, useClass: SpanishPaginatorIntl }],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.Emulated
})
export class TaskHistoryComponent implements AfterViewInit, OnInit {
  private taskHistoryService = inject(TaskHistoryService);
  private route = inject(ActivatedRoute);

  displayedColumns: string[] = [];
  dataSource = new MatTableDataSource<any>();
  docId: number = 0;

  private fontLink: HTMLLinkElement | null = null;
  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;

  constructor(private renderer: Renderer2) {
  }

  ngAfterViewInit() {
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
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
      if (docIdParam) {
        this.docId = +docIdParam;
        this.taskHistoryOnClick();
      }
    });
    this.fontLink = this.renderer.createElement('link');
    if (this.fontLink) {
      this.fontLink.rel = 'stylesheet';
      this.fontLink.href = 'https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500&display=swap';
      this.renderer.appendChild(document.head, this.fontLink);
    }
  }

  taskHistoryOnClick(): void {
    this.taskHistoryService.getTaskHistory(this.docId)
      .pipe(
        tap(response => {
          console.log(response);
          if (response != undefined) {
            response = JSON.parse(response);
            this.dataSource.data = response.data;
            this.displayedColumns = response.columnNames;
          }
        }),
        catchError(error => {
          console.error('Error fetching task history:', error);
          return of([]);
        })
      ).subscribe();
  }

  indexesHistoryOnClick(): void {
    this.taskHistoryService.getIndexesHistory(this.docId)
      .pipe(
        tap(response => {
          console.log(response);
          if (response != undefined) {
            response = JSON.parse(response);
            this.dataSource.data = response.data;
            this.displayedColumns = response.columnNames;
          }
        }),
        catchError(error => {
          console.error('Error fetching indexes history:', error);
          return of([]);
        })
      ).subscribe();
  }
}
