import { Component, inject, Input } from '@angular/core';
import { IWigetConfiguration } from '../interfaces';
import {
  AUTH_SERVICE,
  CLAIMS_PERMISSION_SERVICE,
  IAuthService,
  IClaimsPermissionService,
  IRolePermissionService,
  ROLE_PERMISSION_SERVICE,
} from '@horns-and-hooves/core-auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'horns-and-hooves-widget-container',
  imports: [CommonModule],
  templateUrl: './widget-container.component.html',
  styleUrl: './widget-container.component.scss',
})
export class WidgetContainerComponent {
  @Input() widgets: IWigetConfiguration[] = [];

  private readonly _autService: IAuthService = inject(AUTH_SERVICE);

  private readonly _rolePermissionsService: IRolePermissionService = inject(
    ROLE_PERMISSION_SERVICE
  );

  private readonly _claimsPermissionService: IClaimsPermissionService = inject(
    CLAIMS_PERMISSION_SERVICE
  );

  get visibleWidgets(): IWigetConfiguration[] {
    return this.widgets
      .filter((w) => {
        if (
          w.allowForUnauthorized == false &&
          !this._autService.isAuthenticated()
        ) {
          return false;
        }

        if (
          w.roles &&
          w.roles.length > 0 &&
          !this._rolePermissionsService.hasRoles(w.roles)
        )
          return false;

        if (
          w.claims &&
          w.claims.length > 0 &&
          !this._claimsPermissionService.hasClaims(w.claims)
        )
          return false;

        return true;
      })
      .sort((a, b) => (a.order || 0) - (b.order || 0));
  }
}
