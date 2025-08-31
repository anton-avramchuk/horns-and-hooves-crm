import { TestBed } from '@angular/core/testing';
import { MenuService } from './menu.service';
import { MENU_PROVIDER } from '../horns-and-hooves-core-layout.module';
import { IMenuItem, IMenuProvider } from './interfaces';

describe('MenuService', () => {
  let service: MenuService;

  const mockProviderA: IMenuProvider = {
    key: 'providerA',
    getMenuItems: () => [{ label: 'Item A1' }, { label: 'Item A2' }] as IMenuItem[],
  };

  const mockProviderB: IMenuProvider = {
    key: 'providerB',
    getMenuItems: () => [{ label: 'Item B1' }] as IMenuItem[],
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        MenuService,
        {
          provide: MENU_PROVIDER,
          useValue: [mockProviderA, mockProviderB],
        },
      ],
    });

    service = TestBed.inject(MenuService);
  });

  it('должен создаваться', () => {
    expect(service).toBeTruthy();
  });

  it('должен находить меню по ключу', () => {
    const items = service.getMenu('providerA');
    expect(items.length).toBe(2);
    expect(items[0].label).toBe('Item A1');
  });

  it('должен находить другое меню по другому ключу', () => {
    const items = service.getMenu('providerB');
    expect(items.length).toBe(1);
    expect(items[0].label).toBe('Item B1');
  });

  it('должен вернуть пустой массив, если провайдер не найден', () => {
    const items = service.getMenu('unknown');
    expect(items).toEqual([]);
  });
});
