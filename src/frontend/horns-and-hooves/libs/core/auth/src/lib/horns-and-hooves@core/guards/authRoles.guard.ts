import { inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot } from '@angular/router';
import { IAuthService, IRolePermissionService } from '../services';
import { AuthNavigationService } from '../services/auth.navigation.service';
import { AUTH_SERVICE, ROLE_PERMISSION_SERVICE } from '../crm-core-auth.module';

@Injectable()
export class AuthRolesGuard {


    private authService: IAuthService = inject(AUTH_SERVICE);
    private roleService: IRolePermissionService = inject(ROLE_PERMISSION_SERVICE);
    private navigationService: AuthNavigationService = inject(AuthNavigationService);



    canActivate(route: ActivatedRouteSnapshot): boolean {
        const requiredRoles = route.data['roles'] as Array<string>;
        // Проверяем, авторизован ли пользователь и имеет ли необходимые роли
        if (this.authService.isAuthenticated() && this.roleService.hasRoles(requiredRoles)) {
            return true; // Позволяет активировать маршрут
        } else {
            // Если пользователь не авторизован или не имеет необходимых ролей, перенаправляем его на страницу входа
            this.navigationService.navigateToLogin();
            return false; // Запрещает активировать маршрут
        }
    }
}
