import { inject, Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  /**
   *
   */

  private router = inject(Router);

  private route = inject(ActivatedRoute);

  public navigate(url: string) {
    this.router.navigate([url]);
  }

  public navigateToHome() {
    this.router.navigate(['/']);
  }

  public navigateWithRedirectTo(url: string) {
    // Получаем текущий URL
    const currentUrl = this.router.url;

    // Перенаправляем на указанный URL и добавляем текущий URL как параметр redirectTo
    this.router.navigate([url], {
      queryParams: { redirectTo: currentUrl },
    });
  }

  public redirectToSavedUrl() {
    const redirectTo = this.route.snapshot.queryParamMap.get('redirectTo');

    if (redirectTo) {
      this.router.navigate([redirectTo]);
    } else {
      this.navigateToHome();
    }
  }
}
