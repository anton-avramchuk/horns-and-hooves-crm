import {
  Directive,
  inject,
  Inject,
  Input,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import {
  AUTH_SERVICE,
  CLAIMS_PERMISSION_SERVICE,
} from '../crm-core-auth.module';
import { IAuthService, IClaimsPermissionService } from '../services';

@Directive({
  selector: '[crmHasClaimsPermission]',
})
export class CrmClaimsHasPermissionDirective {
  private authService: IAuthService = inject(AUTH_SERVICE);
  private claimsService: IClaimsPermissionService = inject(
    CLAIMS_PERMISSION_SERVICE
  );

  private templateRef: TemplateRef<any> = inject(TemplateRef);
  private viewContainer: ViewContainerRef = inject(ViewContainerRef);


  @Input() set crmHasClaimsPermission(requiredPermissions: string[]) {
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
      this.claimsService.hasClaims(requiredPermissions)
    );
  }
}
