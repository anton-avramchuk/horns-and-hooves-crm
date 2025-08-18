import { inject, Injectable } from '@angular/core';
import { IAuthService } from '../services';
import { AuthNavigationService } from '../services/auth.navigation.service';
import { AUTH_SERVICE } from '../crm-core-auth.module';

@Injectable()
export class AuthGuard {
  private authService: IAuthService = inject(AUTH_SERVICE);

  private navigationService: AuthNavigationService = inject(
    AuthNavigationService
  );


  canActivate(): boolean {
    // Проверяем, авторизован ли пользователь и имеет ли необходимые роли
    if (this.authService.isAuthenticated()) {
      return true; // Позволяет активировать маршрут
    } else {
      // Если пользователь не авторизован или не имеет необходимых ролей, перенаправляем его на страницу входа
      this.navigationService.navigateToLogin();
      return false; // Запрещает активировать маршрут
    }
  }
}
