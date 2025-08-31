import {
  InjectionToken,
  ModuleWithProviders,
  NgModule,
  Optional,
  Provider,
  SkipSelf,
  Type,
} from '@angular/core';
import { IMenuProvider } from './menu';
import {
  HornsAndHoovesCoreModule,
  throwIfAlreadyLoaded,
} from '@horns-and-hooves/core';
export const MENU_PROVIDER = new InjectionToken<IMenuProvider>(
  'crmMenuProvider'
);

export interface ILayoutModuleConfig {
  menuProviders: Type<IMenuProvider>[];
}

@NgModule({
  declarations: [],
  imports: [HornsAndHoovesCoreModule],
  exports: [],
  providers: [],
})
export class HornsAndHoovesCoreLayoutModule {
  constructor(@Optional() @SkipSelf() parentModule: HornsAndHoovesCoreLayoutModule) {
    throwIfAlreadyLoaded(parentModule, 'HornsAndHoovesCoreModule');
  }

  static forRoot(
    config: ILayoutModuleConfig
  ): ModuleWithProviders<HornsAndHoovesCoreLayoutModule> {
    const providers: Provider[] = config.menuProviders.map((provider) => ({
      provide: MENU_PROVIDER,
      useClass: provider,
      multi: true,
    }));

    return {
      ngModule: HornsAndHoovesCoreLayoutModule,
      providers: [providers],
    };
  }
}
