import { inject, Injectable } from '@angular/core';
import { IMenuItem, IMenuProvider } from './interfaces';
import { MENU_PROVIDER } from '../crm-core-layout.module';

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  private readonly providers = inject<IMenuProvider[]>(MENU_PROVIDER);

  getMenu(key: string): IMenuItem[] {
    const provider = this.providers.find((p) => p.key === key);
    return provider ? provider.getMenuItems() : [];
  }
}
