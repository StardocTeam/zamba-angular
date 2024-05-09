import {
  ChangeDetectorRef,
  Component,
  Inject,
  Optional,
  OnInit,
  LOCALE_ID,
  ElementRef,
  Input,
  EventEmitter,
  OnDestroy
} from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { StartupService } from '@core';
import { ReuseTabService } from '@delon/abc/reuse-tab';
import { DA_SERVICE_TOKEN, ITokenService, SocialService } from '@delon/auth';
import { SettingsService, _HttpClient } from '@delon/theme';
import { CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarView } from 'angular-calendar';
import { GridsterItem, GridsterItemComponent } from 'angular-gridster2';
import { EventColor } from 'calendar-utils';
import { isSameDay, isSameMonth, parse } from 'date-fns';
import { es } from 'date-fns/locale';
import { Subject, Subscription } from 'rxjs';
import { __param } from 'tslib';

import { CalendarService } from './service/calendar.service';

interface MyCalendarEvent extends CalendarEvent {
  groupid: any;
  userid: any;
}

const colors: Record<string, EventColor> = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  }
};

@Component({
  selector: 'calendar-widget',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.less'],
  providers: [SocialService]
  //changeDetection: ChangeDetectionStrategy.OnPush
})
export class CalendarComponent implements OnInit, OnDestroy {
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
  gridItem: ElementRef | undefined;

  private resizeSubscription: Subscription | undefined;

  groups: any[] = [];
  viewDate: Date = new Date();
  events: MyCalendarEvent[] = [];

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  locale: string = es?.code || 'en-US';

  refresh = new Subject<void>();

  isVisible = false;

  activeDayIsOpen: boolean = false;

  //event modal properties
  eventTitle: string = '';
  start: Date = new Date();
  end: Date = new Date();

  selectedValue: any = null;

  eventPrimaryColor: string = '#ad2121';

  eventSecondaryColor: string = '#cccccc';

  eventTextColor: string = '#000000';

  modalHeaderTitle: string = 'Agregar Evento';

  isEditMode: boolean = false;

  editableEventCalendar: MyCalendarEvent = {
    title: '',
    start: new Date(),
    end: new Date(),
    color: { primary: '', secondary: '', secondaryText: '' },
    actions: [],
    draggable: false,
    resizable: {
      beforeStart: false,
      afterEnd: false
    },
    groupid: null,
    userid: null
  };

  actions: CalendarEventAction[] = [
    {
      label: '<span> &bull; Editar </span>',
      a11yLabel: 'Edit',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.showModalEdit(event);
      }
    },
    {
      label: '<span> &bull; Eliminar </span>',
      a11yLabel: 'Delete',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        var genericRequest = {
          UserId: null,
          Params: {
            calendareventid: event.id
          }
        };
        this.calendarService.deleteEvent(genericRequest).subscribe(data => {
          this.events = this.events.filter(iEvent => iEvent !== event);
          this.refresh.next();
          console.log('El evento ha sido eliminado!', event);
        });
      }
    }
  ];

  setView(view: CalendarView) {
    this.view = view;
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if ((isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) || events.length === 0) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  eventTimesChanged({ event, newStart, newEnd }: CalendarEventTimesChangedEvent): void {
    event.start = newStart;
    event.end = newEnd;
    this.refresh.next();
  }

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private settingsService: SettingsService,
    private socialService: SocialService,
    @Optional()
    @Inject(ReuseTabService)
    private reuseTabService: ReuseTabService,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private startupSrv: StartupService,
    private http: _HttpClient,
    private cdr: ChangeDetectorRef,

    @Inject(LOCALE_ID) locale: string,
    private calendarService: CalendarService
  ) {}

  ngOnInit() {
    var groupids = [];
    const tokenData = this.tokenService.get();
    if (tokenData != null) {
      this.groups = [...tokenData['groups']];
      groupids = this.groups.map(group => group.ID);
      this.groups.unshift({ ID: -100, Name: 'Mi Calendario' });
    }
    var genericRequest = {
      UserId: null,
      Params: {
        groupids: JSON.stringify(groupids),
        userid: tokenData ? tokenData['userID'] : null
      }
    };

    this.calendarService.getEvents(genericRequest).subscribe(data => {
      let calendarEvents = JSON.parse(data);
      console.log('Datos recibidos de getEvents:', calendarEvents);
      calendarEvents = calendarEvents.map((event: any) => ({
        ...event,
        start: new Date(event.start),
        end: new Date(event.end),
        actions: this.actions
      }));

      this.events = [...this.events, ...calendarEvents];
      this.refresh.next();
      this.cdr.detectChanges();
    });

    this.resizeSubscription = this.resizeEvent.subscribe((event: any) => {
      let itemComponent: GridsterItemComponent = event['itemComponent'];
      let item: GridsterItemComponent = event['item'];
      this.cdr.detectChanges();
    });
  }
  ngOnDestroy() {
    if (this.resizeSubscription) {
      this.resizeSubscription.unsubscribe();
    }
  }

  handleEvent(action: string, event: CalendarEvent): void {
    /*
    this.modalData = { event, action };
    this.modal.open(this.modalContent, { size: 'lg' });
    */
  }

  handleOk(): void {
    console.log('Button ok clicked!');
    this.isVisible = false;
  }

  handleCancel(): void {
    console.log('Button cancel clicked!');
    this.isVisible = false;
  }
  showModal(event: Event, date: Date): void {
    this.isEditMode = false;
    this.modalHeaderTitle = 'Agregar Evento';
    this.isVisible = true;
    this.eventTitle = '';
    this.eventPrimaryColor = '#ad2121';
    this.eventSecondaryColor = '#cccccc';
    this.eventTextColor = '#000000';
    this.start = date;
    this.end = new Date(date.getTime());
    this.end.setHours(this.start.getHours() + 1);
    this.selectedValue = -100;
    console.log('valor de date: ', date);
    event.preventDefault();
  }

  showModalEdit(event: any): void {
    this.isEditMode = true;
    this.modalHeaderTitle = 'Editar Evento';
    this.isVisible = true;
    this.eventTitle = event.title;
    this.eventPrimaryColor = event.color?.primary || this.eventPrimaryColor;
    this.eventSecondaryColor = event.color?.secondary || this.eventSecondaryColor;
    this.eventTextColor = event.color?.secondaryText || this.eventTextColor;
    this.start = event.start || new Date();
    this.end = event.end || new Date();
    this.end.setHours(this.start.getHours() + 1);
    this.editableEventCalendar = event;
    this.selectedValue = -100;
    if (event['groupid'] != null) {
      this.selectedValue = event['groupid'];
    }
  }
  addEvent(): void {
    let start: Date;
    let end: Date;

    const tokenData = this.tokenService.get();

    if (this.start instanceof Date) {
      start = this.start;
    } else {
      let formatString = 'dd/MM/yyyy HH:mm';
      start = parse(this.start, formatString, new Date());
    }

    if (this.end instanceof Date) {
      end = this.end;
    } else {
      let formatString = 'dd/MM/yyyy HH:mm';
      end = parse(this.end, formatString, new Date());
    }

    //colors
    let color: EventColor = {
      primary: this.eventPrimaryColor,
      secondary: this.eventSecondaryColor,
      secondaryText: this.eventTextColor
    };
    let groupid = null;
    let userid = null;

    if (this.selectedValue == -100) {
      if (tokenData != null) userid = tokenData['userID'];
    } else {
      groupid = this.selectedValue;
    }
    let newEvent = {
      id: 0,
      title: this.eventTitle,

      start: start,
      end: end,
      actions: this.actions,
      color: color,
      draggable: false,
      resizable: {
        beforeStart: false,
        afterEnd: false
      },
      groupid: groupid,
      userid: userid
    };

    var genericRequest = {
      UserId: tokenData ? tokenData['userID'] : null,
      Params: {
        groupid: groupid,
        eventdata: JSON.stringify(newEvent),
        userid: userid
      }
    };

    this.calendarService.insertNewEvent(genericRequest).subscribe(data => {
      console.log('Datos recibidos de insertar un evento:', data);
      newEvent.id = data;
      this.events = [...this.events, newEvent];
      this.refresh.next();
      this.isVisible = false;
      console.log(this.events);
      console.log(JSON.stringify(this.events));
    });
  }

  editEvent(): void {
    let start: Date;
    let end: Date;

    const tokenData = this.tokenService.get();

    if (this.start instanceof Date) {
      start = this.start;
    } else {
      let formatString = 'dd/MM/yyyy HH:mm';
      start = parse(this.start, formatString, new Date());
    }

    if (this.end instanceof Date) {
      end = this.end;
    } else {
      let formatString = 'dd/MM/yyyy HH:mm';
      end = parse(this.end, formatString, new Date());
    }

    //colors
    let color: EventColor = {
      primary: this.eventPrimaryColor,
      secondary: this.eventSecondaryColor,
      secondaryText: this.eventTextColor
    };

    this.editableEventCalendar.title = this.eventTitle;
    this.editableEventCalendar.start = start;
    this.editableEventCalendar.end = end;
    this.editableEventCalendar.color = color;

    let groupid = null;
    let userid = null;

    if (this.selectedValue == -100) {
      if (tokenData != null) userid = tokenData['userID'];
    } else {
      groupid = this.selectedValue;
    }

    this.editableEventCalendar.groupid = groupid;
    this.editableEventCalendar.userid = userid;

    var genericRequest = {
      UserId: tokenData ? tokenData['userID'] : null,
      Params: {
        groupid: groupid,
        eventdata: JSON.stringify(this.editableEventCalendar),
        userid: userid,
        calendareventid: this.editableEventCalendar.id
      }
    };

    this.calendarService.updateEvent(genericRequest).subscribe(data => {
      this.refresh.next();
      this.isVisible = false;
    });
  }
}
