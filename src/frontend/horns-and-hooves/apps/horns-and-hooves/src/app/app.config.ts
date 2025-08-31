import {
  ApplicationConfig,
  provideBrowserGlobalErrorListeners,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app.routes';
import { ILayoutAppConfig } from '@horns-and-hooves/ui-layout';
import { ICrmCoreModuleConfig } from '@horns-and-hooves/core';
import { IAuthModuleConfig } from '@horns-and-hooves/core-auth';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(appRoutes),
  ],
};

export const layoutModuleConfig: ILayoutAppConfig = {
  inputStyle: 'outlined',
  colorScheme: 'light',
  theme: 'lara-light-indigo',
  ripple: false,
  menuMode: 'static',
  scale: 14,
};

export const applicationConfig: ICrmCoreModuleConfig = {
  applicationName: 'Horns and Hooves CRM',
  apiUrl: 'http://localhost:3000/api',
  localStorageKey: 'horns-and-hooves-crm',
};

export const menuProviders = [];

export const authConfig: IAuthModuleConfig = {};
