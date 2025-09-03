import { Component, inject } from '@angular/core';
import { HornsAndHoovesLayoutService } from '../../services';
import { FooterComponent } from '../../footer/footer.component';
import { HeaderComponent } from '../../header/header.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SidebarComponent } from '../../sidebar/sidebar.component';

@Component({
  imports: [
    FooterComponent,
    HeaderComponent,
    CommonModule,
    RouterModule,
    SidebarComponent,
  ],
  templateUrl: './fixed-menu-layout.component.html',
  styleUrl: './fixed-menu-layout.component.scss',
})
export class FixedMenuLayoutComponent {
  private readonly layoutService = inject(HornsAndHoovesLayoutService);

  get containerClass() {
    return {
      'layout-theme-light': this.layoutService.config().colorScheme === 'light',
      'layout-theme-dark': this.layoutService.config().colorScheme === 'dark',
      'layout-overlay': this.layoutService.config().menuMode === 'overlay',
      'layout-static': this.layoutService.config().menuMode === 'static',
      'layout-static-inactive':
        this.layoutService.state.staticMenuDesktopInactive &&
        this.layoutService.config().menuMode === 'static',
      'layout-overlay-active': this.layoutService.state.overlayMenuActive,
      'layout-mobile-active': this.layoutService.state.staticMenuMobileActive,
      'p-input-filled': this.layoutService.config().inputStyle === 'filled',
      'p-ripple-disabled': !this.layoutService.config().ripple,
    };
  }
}
