import {
  InjectionToken,
  ModuleWithProviders,
  NgModule,
  Provider,
  Type,
} from '@angular/core';
import { IMenuProvider } from './menu';
export const MENU_PROVIDER = new InjectionToken<IMenuProvider>(
  'crmMenuProvider'
);

export interface ILayoutModuleConfig {
  menuProviders: Type<IMenuProvider>[];
}

@NgModule({
  declarations: [],
  imports: [],
  exports: [],
  providers: [],
})
export class CrmCoreLayoutModule {
  static forRoot(
    config: ILayoutModuleConfig
  ): ModuleWithProviders<CrmCoreLayoutModule> {
    const providers: Provider[] = config.menuProviders.map((provider) => ({
      provide: MENU_PROVIDER,
      useClass: provider,
      multi: true,
    }));

    return {
      ngModule: CrmCoreLayoutModule,
      providers: [providers],
    };
  }
}
