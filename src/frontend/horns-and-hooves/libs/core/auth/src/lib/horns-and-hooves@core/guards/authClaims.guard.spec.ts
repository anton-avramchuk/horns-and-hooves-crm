import { TestBed } from '@angular/core/testing';
import { AuthClaimsGuard } from './authClaims.guard';
import { ActivatedRouteSnapshot } from '@angular/router';
import { AuthNavigationService } from '../services';
import { AUTH_SERVICE, CLAIMS_PERMISSION_SERVICE } from '../crm-core-auth.module';

describe('AuthClaimsGuard', () => {
    let guard: AuthClaimsGuard;
    let authServiceMock: any;
    let claimsPermissionServiceMock: any;
    let navigationServiceMock: any;
    let routeSnapshotMock: Partial<ActivatedRouteSnapshot>;

    beforeEach(() => {
        authServiceMock = {
            isAuthenticated: jest.fn()
        };

        claimsPermissionServiceMock = {
            hasClaims: jest.fn()
        };

        navigationServiceMock = {
            navigateToLogin: jest.fn()
        };

        routeSnapshotMock = {
            data: {
                claims: ['claim1', 'claim2']
            }
        };

        TestBed.configureTestingModule({
            providers: [
                AuthClaimsGuard,
                { provide: AUTH_SERVICE, useValue: authServiceMock },
                { provide: CLAIMS_PERMISSION_SERVICE, useValue: claimsPermissionServiceMock },
                { provide: AuthNavigationService, useValue: navigationServiceMock }
            ]
        });

        guard = TestBed.inject(AuthClaimsGuard);
    });

    it('should be created', () => {
        expect(guard).toBeTruthy();
    });

    it('should allow activation if user is authenticated and has necessary claims', () => {
        authServiceMock.isAuthenticated.mockReturnValue(true);
        claimsPermissionServiceMock.hasClaims.mockReturnValue(true);

        expect(guard.canActivate(routeSnapshotMock as ActivatedRouteSnapshot)).toBe(true);
        expect(navigationServiceMock.navigateToLogin).not.toHaveBeenCalled();
    });

    it('should navigate to login page if user is not authenticated', () => {
        authServiceMock.isAuthenticated.mockReturnValue(false);
        claimsPermissionServiceMock.hasClaims.mockReturnValue(true);

        expect(guard.canActivate(routeSnapshotMock as ActivatedRouteSnapshot)).toBe(false);
        expect(navigationServiceMock.navigateToLogin).toHaveBeenCalled();
    });

    it('should navigate to login page if user does not have necessary claims', () => {
        authServiceMock.isAuthenticated.mockReturnValue(true);
        claimsPermissionServiceMock.hasClaims.mockReturnValue(false);

        expect(guard.canActivate(routeSnapshotMock as ActivatedRouteSnapshot)).toBe(false);
        expect(navigationServiceMock.navigateToLogin).toHaveBeenCalled();
    });

    it('should navigate to login page if user is not authenticated and does not have necessary claims', () => {
        authServiceMock.isAuthenticated.mockReturnValue(false);
        claimsPermissionServiceMock.hasClaims.mockReturnValue(false);

        expect(guard.canActivate(routeSnapshotMock as ActivatedRouteSnapshot)).toBe(false);
        expect(navigationServiceMock.navigateToLogin).toHaveBeenCalled();
    });
});
