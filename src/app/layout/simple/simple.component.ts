import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { SettingsService, User } from '@delon/theme';
import { LayoutDefaultOptions } from '@delon/theme/layout-default';
import { Subject } from 'rxjs';
import { filter, map, takeUntil } from 'rxjs/operators';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'layout-simple',
  templateUrl: './simple.component.html',
  styleUrls: ['./simple.component.less'],
})
export class LayoutSimpleComponent implements OnInit, OnDestroy {
  pageTitle = '';
  private destroy$ = new Subject<void>();

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private settings: SettingsService,
    private message: MessageService,
  ) {}

  ngOnInit(): void {
    this.settings.setLayout('collapsed', true);
    this.message.startListening();

    const getLeaf = (r: ActivatedRoute) => {
      while (r.firstChild) r = r.firstChild;
      return r;
    };

    // Inicial
    let r = getLeaf(this.route);
    this.pageTitle = r.snapshot.data['title'] ?? (r.snapshot.routeConfig?.path?.replace(/-/g, ' ') || '');

    // En navegación
    this.router.events
      .pipe(
        filter((e): e is NavigationEnd => e instanceof NavigationEnd),
        map(() => getLeaf(this.route)),
        takeUntil(this.destroy$),
      )
      .subscribe(last => {
        this.pageTitle = last.snapshot.data['title'] ?? (last.snapshot.routeConfig?.path?.replace(/-/g, ' ') || '');
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  options: LayoutDefaultOptions = {
    logoExpanded: `./assets/logo-zamba-rrhh-t.svg`,
    logoCollapsed: `./assets/logo-zamba-rrhh-iso.png`,
    hideAside: true,
    logoLink: '/',
  };
  searchToggleStatus = false;
  showSettingDrawer = false;
  get user(): User {
    return this.settings.user;
  }
}
