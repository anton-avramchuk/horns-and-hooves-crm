import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { Subject } from 'rxjs';
import {
  ILayoutAppConfig,
  LAYOUT_APP_PROVIDER,
} from '../horns-and-hooves-layout.module';
export interface ILayoutState {
  staticMenuDesktopInactive: boolean;
  overlayMenuActive: boolean;
  profileSidebarVisible: boolean;
  configSidebarVisible: boolean;
  staticMenuMobileActive: boolean;
  menuHoverActive: boolean;
}
@Injectable()
export class HornsAndHoovesLayoutService {
  _config: ILayoutAppConfig = inject(LAYOUT_APP_PROVIDER);

  config: WritableSignal<ILayoutAppConfig>;

  /**
   *
   */
  constructor() {
    this.config = signal<ILayoutAppConfig>(this._config);
  }

  state: ILayoutState = {
    staticMenuDesktopInactive: false,
    overlayMenuActive: false,
    profileSidebarVisible: false,
    configSidebarVisible: false,
    staticMenuMobileActive: false,
    menuHoverActive: false,
  };

  private configUpdate = new Subject<ILayoutAppConfig>();

  private overlayOpen = new Subject<any>();

  configUpdate$ = this.configUpdate.asObservable();

  overlayOpen$ = this.overlayOpen.asObservable();

  updateStyle(config: ILayoutAppConfig) {
    return (
      config.theme !== this._config.theme ||
      config.colorScheme !== this._config.colorScheme
    );
  }

  onMenuToggle() {
    if (this.isOverlay()) {
      this.state.overlayMenuActive = !this.state.overlayMenuActive;
      if (this.state.overlayMenuActive) {
        this.overlayOpen.next(null);
      }
    }

    if (this.isDesktop()) {
      this.state.staticMenuDesktopInactive =
        !this.state.staticMenuDesktopInactive;
    } else {
      this.state.staticMenuMobileActive = !this.state.staticMenuMobileActive;

      if (this.state.staticMenuMobileActive) {
        this.overlayOpen.next(null);
      }
    }
  }

  showProfileSidebar() {
    this.state.profileSidebarVisible = !this.state.profileSidebarVisible;
    if (this.state.profileSidebarVisible) {
      this.overlayOpen.next(null);
    }
  }

  showConfigSidebar() {
    this.state.configSidebarVisible = true;
  }

  isOverlay() {
    return this.config().menuMode === 'overlay';
  }

  isDesktop() {
    return window.innerWidth > 991;
  }

  isMobile() {
    return !this.isDesktop();
  }

  onConfigUpdate() {
    this._config = { ...this.config() };
    this.configUpdate.next(this.config());
  }

  changeTheme() {
    const { theme, colorScheme } = this.config();
    const themeLink = document.getElementById(
      'theme-css'
    ) as HTMLLinkElement | null;
    if (!themeLink) return;

    const href = themeLink.getAttribute('href') ?? '';
    const newHref = href
      .replace(new RegExp(`/${this._config.theme}/`), `/${theme}/`)
      .replace(
        new RegExp(`theme-${this._config.colorScheme}\\.css$`),
        `theme-${colorScheme}.css`
      );

    this.replaceThemeLink(newHref);
  }
  replaceThemeLink(href: string) {
    const id = 'theme-css';
    const themeLink = <HTMLLinkElement>document.getElementById(id);
    const cloneLinkElement = <HTMLLinkElement>themeLink.cloneNode(true);

    cloneLinkElement.setAttribute('href', href);
    cloneLinkElement.setAttribute('id', id + '-clone');

    themeLink.parentNode!.insertBefore(cloneLinkElement, themeLink.nextSibling);
    cloneLinkElement.addEventListener('load', () => {
      themeLink.remove();
      cloneLinkElement.setAttribute('id', id);
    });
  }

  changeScale(value: number) {
    document.documentElement.style.fontSize = `${value}px`;
  }
}
