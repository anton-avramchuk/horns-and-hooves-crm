import { TestBed } from '@angular/core/testing';
import {
  LAYOUT_APP_PROVIDER,
  ILayoutAppConfig,
} from '../horns-and-hooves-layout.module';
import { HornsAndHoovesLayoutService } from './layout.service';

describe('HornsAndHoovesLayoutService (Jest)', () => {
  let service: HornsAndHoovesLayoutService;
  let config: ILayoutAppConfig;

  beforeEach(() => {
    // Чистим DOM перед каждым тестом
    document.body.innerHTML = '';

    config = {
      inputStyle: 'outlined',
      colorScheme: 'light',
      theme: 'lara-light-indigo',
      ripple: true,
      menuMode: 'static',
      scale: 14,
    };

    TestBed.configureTestingModule({
      providers: [
        HornsAndHoovesLayoutService,
        { provide: LAYOUT_APP_PROVIDER, useValue: config },
      ],
    });

    service = TestBed.inject(HornsAndHoovesLayoutService);
  });

  afterEach(() => {
    document.body.innerHTML = '';
    jest.restoreAllMocks();
  });

  it('должен создаваться', () => {
    expect(service).toBeTruthy();
  });

  it('updateStyle возвращает true если поменялись theme или colorScheme', () => {
    const newConfig = { ...config, theme: 'new-theme' };
    expect(service.updateStyle(newConfig)).toBe(true);

    const sameConfig = { ...config };
    expect(service.updateStyle(sameConfig)).toBe(false);
  });

  it('onMenuToggle переключает overlayMenuActive при overlay режиме', () => {
    // подменим config на overlay
    service.config.update((cfg) => ({
      ...cfg,
      menuMode: 'overlay',
    }));

    service.onMenuToggle();
    expect(service.state.overlayMenuActive).toBe(true);

    service.onMenuToggle();
    expect(service.state.overlayMenuActive).toBe(false);
  });

  it('onMenuToggle переключает staticMenuDesktopInactive при desktop', () => {
    jest.spyOn(service, 'isDesktop').mockReturnValue(true);
    jest.spyOn(service, 'isOverlay').mockReturnValue(false);

    service.onMenuToggle();
    expect(service.state.staticMenuDesktopInactive).toBe(true);
  });

  it('onMenuToggle переключает staticMenuMobileActive при mobile', () => {
    jest.spyOn(service, 'isDesktop').mockReturnValue(false);
    jest.spyOn(service, 'isOverlay').mockReturnValue(false);

    service.onMenuToggle();
    expect(service.state.staticMenuMobileActive).toBe(true);
  });

  it('showProfileSidebar переключает profileSidebarVisible', () => {
    expect(service.state.profileSidebarVisible).toBe(false);
    service.showProfileSidebar();
    expect(service.state.profileSidebarVisible).toBe(true);
    service.showProfileSidebar();
    expect(service.state.profileSidebarVisible).toBe(false);
  });

  it('showConfigSidebar включает configSidebarVisible', () => {
    expect(service.state.configSidebarVisible).toBe(false);
    service.showConfigSidebar();
    expect(service.state.configSidebarVisible).toBe(true);
  });

  it('onConfigUpdate обновляет _config и пушит в configUpdate$', (done) => {
    service.config.update((cfg) => ({ ...cfg, theme: 'dark' }));

    service.configUpdate$.subscribe((updated) => {
      expect(updated.theme).toBe('dark');
      done();
    });

    service.onConfigUpdate();
  });

  it('changeScale обновляет размер шрифта root элемента', () => {
    service.changeScale(18);
    expect(document.documentElement.style.fontSize).toBe('18px');
  });

  it('replaceThemeLink заменяет ссылку на тему', () => {
    // создаём элемент в DOM
    const link = document.createElement('link');
    link.id = 'theme-css';
    link.setAttribute('href', 'old-theme.css');
    document.body.appendChild(link);

    service.replaceThemeLink('new-theme.css');

    const clone = document.getElementById('theme-css-clone') as HTMLLinkElement;
    expect(clone).not.toBeNull();
    expect(clone.getAttribute('href')).toBe('new-theme.css');
  });

  it('changeTheme вызывает replaceThemeLink с правильным href', () => {
    // создаём элемент
    const link = document.createElement('link');
    link.id = 'theme-css';
    link.setAttribute('href', '/themes/lara-light-indigo/theme-light.css');
    document.body.appendChild(link);

    service.config.update((cfg) => ({
      ...cfg,
      theme: 'lara-dark-indigo',
      colorScheme: 'dark',
    }));

    const spy = jest.spyOn(service, 'replaceThemeLink').mockImplementation();

    service.changeTheme();

    expect(spy).toHaveBeenCalledWith('/themes/lara-dark-indigo/theme-dark.css');
  });
});
