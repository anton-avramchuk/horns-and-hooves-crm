import {
  InjectionToken,
  ModuleWithProviders,
  NgModule,
  Optional,
  Provider,
  SkipSelf,
} from '@angular/core';
import { throwIfAlreadyLoaded } from './functions';

export const APP_NAME = new InjectionToken<string>('crmAppName');
export const APP_STORAGE_KEY = new InjectionToken<string>('crmLocalStorageKey');
export const APP_API_URL = new InjectionToken<string>('crmApiUrl');
export const LOCAL_STORAGE = new InjectionToken<any>('Browser Storage', {
  providedIn: 'root',
  factory: () => localStorage,
});

export interface ICrmCoreModuleConfig {
  applicationName: string;
  localStorageKey?: string;
  apiUrl: string;
}
@NgModule()
export class HornsAndHoovesCoreModule {
  constructor(@Optional() @SkipSelf() parentModule: HornsAndHoovesCoreModule) {
    throwIfAlreadyLoaded(parentModule, 'HornsAndHoovesCoreModule');
  }
  static forRoot(
    options: ICrmCoreModuleConfig
  ): ModuleWithProviders<HornsAndHoovesCoreModule> {
    const providers: Provider[] = [
      { provide: APP_NAME, useValue: options.applicationName },
      { provide: APP_STORAGE_KEY, useValue: options.localStorageKey ?? 'crm' },
      { provide: APP_API_URL, useValue: options.apiUrl },
    ];
    return {
      ngModule: HornsAndHoovesCoreModule,
      providers: [providers],
    };
  }
}
