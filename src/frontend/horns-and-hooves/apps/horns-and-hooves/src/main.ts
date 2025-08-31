import { bootstrapApplication } from '@angular/platform-browser';
import {
  appConfig,
  layoutModuleConfig,
  applicationConfig,
  menuProviders,
  authConfig,
} from './app/app.config';
import { App } from './app/app';
import { importProvidersFrom } from '@angular/core';
import { HornsAndHoovesLayoutModule } from '@horns-and-hooves/ui-layout';
import { HornsAndHoovesCoreModule } from '@horns-and-hooves/core';
import { HornsAndHoovesCoreLayoutModule } from '@horns-and-hooves/ui-controls-core';
import { HornsAndHoovesCoreAuthModule } from '@horns-and-hooves/core-auth';

bootstrapApplication(App, {
  ...appConfig,
  providers: [
    ...appConfig.providers,
    importProvidersFrom(
      HornsAndHoovesLayoutModule.forRoot({
        layoutConfig: layoutModuleConfig,
      })
    ),
    importProvidersFrom(HornsAndHoovesCoreModule.forRoot(applicationConfig)),
    importProvidersFrom(
      HornsAndHoovesCoreLayoutModule.forRoot({
        menuProviders,
      })
    ),
    importProvidersFrom(HornsAndHoovesCoreAuthModule.forRoot(authConfig)),
  ],
}).catch((err) => console.error(err));
