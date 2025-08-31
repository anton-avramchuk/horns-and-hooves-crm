import { InjectionToken, ModuleWithProviders, NgModule, Provider } from '@angular/core';
import { HornsAndHoovesLayoutService } from './services';

export const LAYOUT_APP_PROVIDER = new InjectionToken<ILayoutAppConfig>(
  'crmPrimeNgLayoutProvider'
);

export interface IPrimengCrmLayoutModuleConfig {
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

@NgModule({
    imports: [],
    exports: [],
    declarations: [],
    providers: [],
})
export class HornsAndHoovesLayoutModule {

    static forRoot(
    config: IPrimengCrmLayoutModuleConfig
  ): ModuleWithProviders<HornsAndHoovesLayoutModule> {

    if (!config.appConfig) {
      config.appConfig = {
        inputStyle: 'outlined',
        colorScheme: 'light',
        theme: 'lara-light-indigo',
        ripple: false,
        menuMode: 'static',
        scale: 14
      }
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
