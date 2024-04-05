import { ChangeDetectionStrategy, ChangeDetectorRef, Component } from '@angular/core';
import { NoticeIconList, NoticeIconSelect, NoticeItem } from '@delon/abc/notice-icon';
import { add, formatDistanceToNow, parse } from 'date-fns';
import { NzI18nService } from 'ng-zorro-antd/i18n';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'header-notify',
  template: `
    <notice-icon
      [data]="data"
      [count]="count"
      [loading]="loading"
      btnClass="alain-default__nav-item"
      btnIconClass="alain-default__nav-item-icon"
      (select)="select($event)"
      (clear)="clear($event)"
      (popoverVisibleChange)="loadData()"
    />
  `,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderNotifyComponent {
  data: NoticeItem[] = [
    {
      title: 'Notification',
      list: [],
      emptyText: 'You have seen all the notifications.',
      emptyImage: 'https://gw.alipayobjects.com/zos/rmsportal/wAhyIChODzsoKIOBHcBk.svg',
      clearText: 'Clear notifications'
    } /*,
    {
      title: 'Info.',
      list: [],
      emptyText: 'You have read all the messages.',
      emptyImage: 'https://gw.alipayobjects.com/zos/rmsportal/sAuJeJzSKbUmHfBQRzmZ.svg',
      clearText: 'Delete messages'
    },
    {
      title: 'Tasks',
      list: [],
      emptyText: 'You have completed all pending tasks',
      emptyImage: 'https://gw.alipayobjects.com/zos/rmsportal/HsIsxMZiWKrNUavQUXqx.svg',
      clearText: 'Delete tasks'
    }*/
  ];
  count = 0;
  loading = false;

  constructor(
    private msg: NzMessageService,
    private nzI18n: NzI18nService,
    private cdr: ChangeDetectorRef
  ) { }

  private updateNoticeData(notices: NoticeIconList[]): NoticeItem[] {
    const data = this.data.slice();
    data.forEach(i => (i.list = []));

    notices.forEach(item => {
      const newItem = { ...item } as NoticeIconList;
      if (typeof newItem.datetime === 'string') {
        newItem.datetime = parse(newItem.datetime, 'yyyy-MM-dd', new Date());
      }
      if (newItem.datetime) {
        newItem.datetime = formatDistanceToNow(newItem.datetime as Date, { locale: this.nzI18n.getDateLocale() });
      }
      if (newItem.extra && newItem['status']) {
        newItem['color'] = (
          {
            todo: undefined,
            processing: 'blue',
            urgent: 'red',
            doing: 'gold'
          } as { [key: string]: string | undefined }
        )[newItem['status']];
      }
      data.find(w => w.title === newItem['type'])!.list.push(newItem);
    });
    return data;
  }

  loadData(): void {
    if (this.loading) {
      return;
    }
    this.loading = true;
    setTimeout(() => {
      const now = new Date();
      this.data = this.updateNoticeData([
        /*
        {
          id: '000000001',
          avatar: 'https://gw.alipayobjects.com/zos/rmsportal/ThXAXghbEsBCCSDihZxY.png',
          title: "You've received 14 new weekly newsletters",
          datetime: add(now, { days: 10 }),
          type: 'Notification'
        },
        {
          id: '000000002',
          avatar: 'https://gw.alipayobjects.com/zos/rmsportal/OKJXDXrmkNshAMvwtvhu.png',
          title: 'Qu Nini, whom you recommended, has passed the third round of interviews',
          datetime: add(now, { days: -3 }),
          type: 'Notification'
        },
        {
          id: '000000003',
          avatar: 'https://gw.alipayobjects.com/zos/rmsportal/kISTdvpyTAhtGxpovNWd.png',
          title: 'This template can differentiate between multiple notification types',
          datetime: add(now, { months: -3 }),
          read: true,
          type: 'Notification'
        },
        {
          id: '000000004',
          avatar: 'https://gw.alipayobjects.com/zos/rmsportal/GvqBnKhFgObvnSGkDsje.png',
          title: 'The icons on the left are used to distinguish different types',
          datetime: add(now, { years: -1 }),
          type: 'Notification'
        },
        {
          id: '000000005',
          avatar: 'https://gw.alipayobjects.com/zos/rmsportal/ThXAXghbEsBCCSDihZxY.png',
          title: 'The content should not exceed two lines. It will be automatically truncated if it exceeds.',
          datetime: '2017-08-07',
          type: 'Notification'
        },
        {
          id: '000000006',
          avatar: 'https://gw.alipayobjects.com/zos/rmsportal/fcHMVNCjPOsbUGdEduuv.jpeg',
          title: 'Qu Lili commented on you',
          description: 'description information description information description information',
          datetime: '2017-08-07',
          type: 'Info.'
        },
        {
          id: '000000007',
          avatar: 'https://gw.alipayobjects.com/zos/rmsportal/fcHMVNCjPOsbUGdEduuv.jpeg',
          title: 'Zhu Qianyou replied to you',
          description: 'This kind of template is used to remind who has interacted with you, and the avatar of "who" is placed on the left',
          datetime: '2017-08-07',
          type: 'Info.'
        },
        {
          id: '000000008',
          avatar: 'https://gw.alipayobjects.com/zos/rmsportal/fcHMVNCjPOsbUGdEduuv.jpeg',
          title: 'Zhu Qianyou replied to you',
          description: 'This kind of template is used to remind who has interacted with you, and the avatar of "who" is placed on the left',
          datetime: '2017-08-07',
          type: 'Info.'
        },
        {
          id: '000000009',
          title: 'Third-party emergency code changes',
          description: 'The task needs to be started before 2017-01-12 20:00',
          extra: 'has not started',
          status: 'todo',
          type: 'Tasks'
        },
        {
          id: '000000010',
          title: 'Third-party emergency code changes',
          description: 'Submitted by Guanlin on 2017-01-06, the code change task needs to be completed before 2017-01-07',
          extra: 'Expires soon',
          status: 'urgent',
          type: 'Tasks'
        },
        {
          id: '000000011',
          title: 'Information Security Exam',
          description: 'Appointed Zhuer to complete the update and release before 2017-01-09',
          extra: 'It took 8 days',
          status: 'doing',
          type: 'Tasks'
        },
        {
          id: '000000012',
          title: 'ABCD version released',
          description: 'Submitted by Guanlin on 2017-01-06, the code change task needs to be completed before 2017-01-07',
          extra: 'in progress',
          status: 'processing',
          type: 'Tasks'
        }*/
      ]);

      this.loading = false;
      this.cdr.detectChanges();
    }, 500);
  }

  clear(type: string): void {
    this.msg.success(`Cleared ${type}`);
  }

  select(res: NoticeIconSelect): void {
    this.msg.success(`Clicked ${res.title} of ${res.item.title}`);
  }
}
