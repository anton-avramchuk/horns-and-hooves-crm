import { inject, Injectable } from '@angular/core';
import { AUTH_LOGIN_PATH } from '../crm-core-auth.module';
import { NavigationService } from '@horns-and-hooves/core';

@Injectable({
  providedIn: 'root',
})
export class AuthNavigationService {
  private loginPath: string = inject(AUTH_LOGIN_PATH);

  private navigationService = inject(NavigationService);

  navigateToLogin() {
    this.navigationService.navigate(this.loginPath);
  }

  navigateToHome() {
    this.navigationService.navigateToHome();
  }
}
