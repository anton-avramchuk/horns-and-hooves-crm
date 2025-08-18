import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { inject, Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AUTH_SERVICE } from '../crm-core-auth.module';
import { AuthNavigationService, IAuthService } from '../services';
import { TokenProvider } from '../providers/token-provider.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {


    private tokenProvider: TokenProvider = inject(TokenProvider);

    private authNavigationService: AuthNavigationService = inject(AuthNavigationService);
    private authService: IAuthService = inject(AUTH_SERVICE);

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (req.headers.get('Content-Type') && req.headers.get('Content-Type')!.indexOf('multipart/form-data') !== -1) {
            req.headers.set('Content-Type', req.headers.get('Content-Type') + '; charset=utf-8');
        }

        if (req.headers.get('No-Auth') === 'True') {
            return next.handle(req.clone());
        }

        if (this.authService.isAuthenticated() && this.tokenProvider.getToken() != null) {
            const clonedreq = req.clone({
                headers: req.headers.set('Authorization', 'Bearer ' + this.tokenProvider.getToken())
            });
            return next.handle(clonedreq).pipe(tap(succ => { },
                err => {
                    if (err.status === 401) {
                        if (this.authService.isAuthenticated()) {
                            this.authService.logOff();
                        }
                        this.onError(err.status);
                    }
                }));
        } else {
            return next.handle(req.clone()).pipe(tap(succ => { },
                err => {
                    this.onError(err.status);

                }));
        }
    }

    private onError(status: unknown): void {
        if (status === 401) {
            this.authNavigationService.navigateToLogin();
        }
    }
}
