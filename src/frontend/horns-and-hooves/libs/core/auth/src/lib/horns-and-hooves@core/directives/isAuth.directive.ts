import {
  Directive,
  inject,
  TemplateRef,
  ViewContainerRef,
  AfterContentInit,
} from '@angular/core';
import { AUTH_SERVICE } from '../crm-core-auth.module';
import { IAuthService } from '../services';

@Directive({
  selector: '[hornsAndHoovesAuthIsAuthDirective]',
})
export class IsAuthDirective implements AfterContentInit {
  private authService: IAuthService = inject(AUTH_SERVICE);
  private templateRef: TemplateRef<any> = inject(TemplateRef);
  private viewContainer: ViewContainerRef = inject(ViewContainerRef);

  private checkPermission(): boolean {
    return this.authService.isAuthenticated();
  }

  ngAfterContentInit() {
    const hasPermission = this.checkPermission();
    if (hasPermission) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainer.clear();
    }
  }
}
