import { Component, OnInit } from '@angular/core';
import { SettingsService, User } from '@delon/theme';
import { LayoutDefaultOptions } from '@delon/theme/layout-default';
import { environment } from '@env/environment';
import { MessageService } from 'src/app/services/message.service';


@Component({
  selector: 'layout-simple',
  templateUrl: './simple.component.html',
  styleUrls: ['./simple.component.less']
})
export class LayoutSimpleComponent implements OnInit {
  constructor(
    private settings: SettingsService,
    private message: MessageService
  ) { }
  ngOnInit(): void {
    this.settings.setLayout('collapsed', true);
    this.message.startListening();
  }

  options: LayoutDefaultOptions = {
    logoExpanded: `./assets/logo-zamba-rrhh-t.svg`,
    logoCollapsed: `./assets/logo-zamba-rrhh-iso.png`,
    hideAside: true,
    logoLink: './taskhistory',
  };
  searchToggleStatus = false;
  showSettingDrawer = false;
  get user(): User {
    return this.settings.user;
  }
}
