import { Component, OnInit } from '@angular/core';
import { SettingsService, User } from '@delon/theme';
import { LayoutDefaultOptions } from '@delon/theme/layout-default';
import { environment } from '@env/environment';

import { ZambaService } from '../../services/zamba/zamba.service';

@Component({
  selector: 'layout-basic',
  templateUrl: './basic.component.html',
  styleUrls: ['./basic.component.less']
})
export class LayoutBasicComponent implements OnInit {
  constructor(
    private settings: SettingsService,
    private ZambaService: ZambaService
  ) { }
  ngOnInit(): void {
    this.ZambaService.GetSidebarItems();
    this.settings.setLayout('collapsed', true);
  }

  options: LayoutDefaultOptions = {
    logoExpanded: `./assets/logo-zamba-rrhh-t.svg`,
    logoCollapsed: `./assets/logo-zamba-rrhh-iso.png`,
    hideAside: false
  };
  searchToggleStatus = false;
  showSettingDrawer = false;
  get user(): User {
    return this.settings.user;
  }
}
