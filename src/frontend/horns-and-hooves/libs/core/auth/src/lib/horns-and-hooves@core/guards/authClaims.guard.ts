import { inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot } from '@angular/router';
import { IAuthService, IClaimsPermissionService } from '../services';
import { AuthNavigationService } from '../services/auth.navigation.service';
import { AUTH_SERVICE, CLAIMS_PERMISSION_SERVICE } from '../crm-core-auth.module';

@Injectable()
export class AuthClaimsGuard {


    private authService: IAuthService = inject(AUTH_SERVICE);
    private roleService: IClaimsPermissionService = inject(CLAIMS_PERMISSION_SERVICE);
    private navigationService: AuthNavigationService = inject(AuthNavigationService);

    

    canActivate(route: ActivatedRouteSnapshot): boolean {
        const claims = route.data['claims'] as Array<string>;
        // Проверяем, авторизован ли пользователь и имеет ли необходимые роли
        if (this.authService.isAuthenticated() && this.roleService.hasClaims(claims)) {
            return true; // Позволяет активировать маршрут
        } else {
            // Если пользователь не авторизован или не имеет необходимых ролей, перенаправляем его на страницу входа
            this.navigationService.navigateToLogin();
            return false; // Запрещает активировать маршрут
        }
    }
}
