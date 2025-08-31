import {
  InjectionToken,
  ModuleWithProviders,
  NgModule,
  Provider,
} from '@angular/core';
import {
  IWigetConfiguration,
  HornsAndHoovesCoreLayoutModule,
} from '@horns-and-hooves/ui-controls-core';
import { HornsAndHoovesLayoutService } from './services';

export const HeaderWidgetInjectionToken = new InjectionToken<IWigetConfiguration[]>(
  'hornsAndHoovesLayoutHeaderWidgets',
  { factory: () => [] }
);

export const LAYOUT_APP_PROVIDER = new InjectionToken<ILayoutAppConfig>(
  'hornsAndHoovesLayoutConfig'
);

export interface IHornsAndHoovesLayoutModuleConfig {
  layoutConfig?: ILayoutAppConfig;
  headerConfig?: IHeaderConfig;
}

export interface ILayoutAppConfig {
  inputStyle: string;
  colorScheme: string;
  theme: string;
  ripple: boolean;
  menuMode: string;
  scale: number;
}

export interface IHeaderConfig {
  topBarConfig: IWigetConfiguration[];
}

@NgModule({
  imports: [HornsAndHoovesCoreLayoutModule],
  exports: [],
  declarations: [],
  providers: [],
})
export class HornsAndHoovesLayoutModule {
  static forRoot(
    config: IHornsAndHoovesLayoutModuleConfig
  ): ModuleWithProviders<HornsAndHoovesLayoutModule> {
    if (!config.layoutConfig) {
      config.layoutConfig = {
        inputStyle: 'outlined',
        colorScheme: 'light',
        theme: 'lara-light-indigo',
        ripple: false,
        menuMode: 'static',
        scale: 14,
      };
    }

    const headerConfig: IHeaderConfig = config.headerConfig ?? {
      topBarConfig: [],
    };

    const headerWidgets: IWigetConfiguration[] =
      headerConfig.topBarConfig ?? [];

    const providers: Provider[] = [
      { provide: HornsAndHoovesLayoutService },
      { provide: LAYOUT_APP_PROVIDER, useValue: config.layoutConfig },
    ];

    headerWidgets.forEach((widget) => {
      providers.push({
        provide: HeaderWidgetInjectionToken,
        useValue: widget,
        multi: true,
      });
    });

    return {
      ngModule: HornsAndHoovesLayoutModule,
      providers: [providers],
    };
  }
}
