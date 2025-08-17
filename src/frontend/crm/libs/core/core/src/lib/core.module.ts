import { NgModule, InjectionToken } from '@angular/core';

export const LOCAL_STORAGE = new InjectionToken<Storage>('LocalStorage');
export const SESSION_STORAGE = new InjectionToken<Storage>('SessionStorage');

@NgModule({
  providers: [
    { provide: LOCAL_STORAGE, useFactory: () => window.localStorage },
    { provide: SESSION_STORAGE, useFactory: () => window.sessionStorage },
  ],
})
export class CoreModule {}
