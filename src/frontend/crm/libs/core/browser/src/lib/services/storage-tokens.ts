import { InjectionToken } from '@angular/core';

export const LOCAL_STORAGE = new InjectionToken<Storage>(
  'window.localStorage',
  { providedIn: 'root', factory: () => localStorage }
);

export const SESSION_STORAGE = new InjectionToken<Storage>(
  'window.sessionStorage',
  { providedIn: 'root', factory: () => sessionStorage }
);