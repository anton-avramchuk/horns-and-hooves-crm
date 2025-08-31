import { Component, inject } from '@angular/core';
import { APP_NAME } from '@horns-and-hooves/core';
import { IWigetConfiguration } from '@horns-and-hooves/ui-controls-core';
import { HeaderWidgetInjectionToken } from '../horns-and-hooves-layout.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'crm-layout-header.component',
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  private _appName: string = inject(APP_NAME);

  private _widgets: IWigetConfiguration[] = inject(HeaderWidgetInjectionToken);
  public get applicationName(): string {
    return this._appName;
  }

  get widgets() {
    return this._widgets.sort((a, b) => (a.order || 0) - (b.order || 0));
  }
}
