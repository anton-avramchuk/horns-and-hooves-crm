import { TestBed } from '@angular/core/testing';
import { AuthGuard } from './auth.guard';
import { AuthNavigationService } from '../services/auth.navigation.service';
import { IAuthService } from '../services';
import { AUTH_SERVICE } from '../crm-core-auth.module';

describe('AuthGuard', () => {
    let guard: AuthGuard;
    let authServiceMock: Partial<IAuthService>;
    let navigationServiceMock: Partial<AuthNavigationService>;

    beforeEach(() => {
        authServiceMock = {
            isAuthenticated: jest.fn()
        };

        navigationServiceMock = {
            navigateToLogin: jest.fn()
        };

        TestBed.configureTestingModule({
            providers: [
                AuthGuard,
                { provide: AUTH_SERVICE, useValue: authServiceMock },
                { provide: AuthNavigationService, useValue: navigationServiceMock }
            ]
        });

        guard = TestBed.inject(AuthGuard);
    });

    it('should be created', () => {
        expect(guard).toBeTruthy();
    });

    it('should allow activation if user is authenticated', () => {
        authServiceMock.isAuthenticated = jest.fn().mockReturnValue(true);
        expect(guard.canActivate()).toBe(true);
        expect(navigationServiceMock.navigateToLogin).not.toHaveBeenCalled();
    });

    it('should navigate to login page if user is not authenticated', () => {
        authServiceMock.isAuthenticated = jest.fn().mockReturnValue(false);
        expect(guard.canActivate()).toBe(false);
        expect(navigationServiceMock.navigateToLogin).toHaveBeenCalled();
    });
});
