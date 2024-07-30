import { ChangeDetectionStrategy, ChangeDetectorRef, EventEmitter, Component, Inject, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { GridsterConfig, GridsterItem } from 'angular-gridster2';
import { NzMessageService } from 'ng-zorro-antd/message';
import { filter, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { WidgetsContainerOptions } from './entities/WidgetsContainerOptions';
import { WidgetsContainerService } from './service/widgets-container.service';

@Component({
  selector: 'widgets-container',
  templateUrl: './widgets-container.component.html',
  styleUrls: ['widgets-container.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class WidgetsContainerComponent implements OnInit {
  options: GridsterConfig = {};
  widgets: GridsterItem[] = [];
  resizeEvent: EventEmitter<any> = new EventEmitter<any>();
  changeEvent: EventEmitter<any> = new EventEmitter<any>();
  constructor(
    public msg: NzMessageService,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private router: Router,
    private WCService: WidgetsContainerService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.getWidgetsContainer();

    this.options = {
      itemChangeCallback: (item, itemComponent) => {
        // console.log("🟥: " + item["type"] + " " + item["cols"] + " " + item["rows"] + " " + item["x"] + " " + item["y"]);
        this.changeEvent.emit(item);
      },
      itemResizeCallback: (item, itemComponent) => {
        // console.log("🟩: " + item["type"] + " " + item["cols"] + " " + item["rows"] + " " + item["x"] + " " + item["y"]);
        this.resizeEvent.emit({ item, itemComponent });
      }
    };

    this.widgets = [];
  }

  getWidgetsContainer() {
    const tokenData: any = this.tokenService.get();
    let genericRequest = {};
    genericRequest = {
      UserId: 0,
      token: tokenData['token'],

      Params: ''
    };

    this.WCService._getWidgetsContainer(genericRequest)
      .pipe(
        filter(data => data != '[]'),
        catchError(error => {
          console.error('An error occurred:', error);
          return of('[]');
        })
      )
      .subscribe(data => {
        var dataJson = JSON.parse(data)[0];
        var optionsJson = JSON.parse(dataJson.Options);

        this.options.displayGrid = optionsJson.displayGrid;
        this.options.draggable = optionsJson.draggable;
        this.options.resizable = optionsJson.resizable;
        this.options.minCols = optionsJson.minCols;
        this.options.minRows = optionsJson.minRows;
        var widgetsList = JSON.parse(dataJson.WidgetCoordinates);

        this.widgets = widgetsList;
        var api: any | null = this.options.api;

        if (api != undefined) {
          api.optionsChanged();
          this.cdr.detectChanges();
        }
      });
  }

  setWidgetsContainer() {
    const tokenData: any = this.tokenService.get();
    let genericRequest = {};

    let WCOptions: WidgetsContainerOptions = new WidgetsContainerOptions(
      this.options.displayGrid,
      this.options.draggable,
      this.options.resizable,
      this.options.minCols,
      this.options.minRows
    );

    genericRequest = {
      UserId: 0, // tokenData['userID'],
      token: tokenData['token'],
      Params: {
        options: JSON.stringify(WCOptions),
        widgetsContainer: JSON.stringify(this.widgets)
      }
    };

    this.WCService._setWidgetsContainer(genericRequest).subscribe(data => {
      this.options = JSON.parse(data.Options);
      this.widgets = JSON.parse(data.WidgetsContainer);
    });
  }

  static itemChange(item: any, itemComponent: any) {
    console.info('itemChanged', item, itemComponent);
  }

  static itemResize(item: any, itemComponent: any) {
    console.info('itemResized', item, itemComponent);
  }

  removeItem(item: any) {
    this.widgets.splice(this.widgets.indexOf(item), 1);
  }
}
