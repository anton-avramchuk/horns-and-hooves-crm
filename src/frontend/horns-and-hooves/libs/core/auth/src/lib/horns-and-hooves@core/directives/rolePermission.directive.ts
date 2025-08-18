import {
  Directive,
  inject,
  Input,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { AUTH_SERVICE, ROLE_PERMISSION_SERVICE } from '../crm-core-auth.module';
import { IAuthService, IRolePermissionService } from '../services';

@Directive({
  selector: '[crmHasRolePermission]',
})
export class CrmRoleHasPermissionDirective {
  private authService: IAuthService = inject(AUTH_SERVICE);
  private roleService: IRolePermissionService = inject(ROLE_PERMISSION_SERVICE);

  private templateRef: TemplateRef<any> = inject(TemplateRef);
  private viewContainer: ViewContainerRef = inject(ViewContainerRef);

  @Input() set crmHasRolePermission(requiredPermissions: string[]) {
    const hasPermission = this.checkPermission(requiredPermissions);

    if (hasPermission) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainer.clear();
    }
  }

  private checkPermission(requiredPermissions: string[]): boolean {
    // Получаем права пользователя из AuthService
    return (
      this.authService.isAuthenticated() &&
      this.roleService.hasRoles(requiredPermissions)
    );
  }
}
