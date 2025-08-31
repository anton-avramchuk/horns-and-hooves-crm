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

export const LAYOUT_APP_PROVIDER = new InjectionToken<ILayoutAppConfig>(
  'hornsAndHoovesLayoutConfig'
);

export interface IHornsAndHoovesLayoutModuleConfig {
  appConfig?: ILayoutAppConfig;
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
  topBarConfig?: IWigetConfiguration[];
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
    if (!config.appConfig) {
      config.appConfig = {
        inputStyle: 'outlined',
        colorScheme: 'light',
        theme: 'lara-light-indigo',
        ripple: false,
        menuMode: 'static',
        scale: 14,
      };
    }

    const providers: Provider[] = [
      { provide: HornsAndHoovesLayoutService },
      { provide: LAYOUT_APP_PROVIDER, useValue: config.appConfig },
    ];

    return {
      ngModule: HornsAndHoovesLayoutModule,
      providers: [providers],
    };
  }
}
