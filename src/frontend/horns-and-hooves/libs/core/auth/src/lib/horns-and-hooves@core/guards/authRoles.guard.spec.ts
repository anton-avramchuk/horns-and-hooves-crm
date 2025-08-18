import { TestBed } from '@angular/core/testing';
import { AuthRolesGuard } from './authRoles.guard';
import { ActivatedRouteSnapshot } from '@angular/router';
import { AuthNavigationService } from '../services';
import { AUTH_SERVICE, ROLE_PERMISSION_SERVICE } from '../crm-core-auth.module';

describe('AuthRolesGuard', () => {
    let guard: AuthRolesGuard;
    let authServiceMock: any;
    let roleServiceMock: any;
    let navigationServiceMock: any;
    let routeSnapshotMock: Partial<ActivatedRouteSnapshot>;

    beforeEach(() => {
        authServiceMock = {
            isAuthenticated: jest.fn()
        };

        roleServiceMock = {
            hasRoles: jest.fn()
        };

        navigationServiceMock = {
            navigateToLogin: jest.fn()
        };

        routeSnapshotMock = {
            data: {
                roles: ['role1', 'role2']
            }
        };

        TestBed.configureTestingModule({
            providers: [
                AuthRolesGuard,
                { provide: AUTH_SERVICE, useValue: authServiceMock },
                { provide: ROLE_PERMISSION_SERVICE, useValue: roleServiceMock },
                { provide: AuthNavigationService, useValue: navigationServiceMock }
            ]
        });

        guard = TestBed.inject(AuthRolesGuard);
    });

    it('should be created', () => {
        expect(guard).toBeTruthy();
    });

    it('should allow activation if user is authenticated and has necessary roles', () => {
        authServiceMock.isAuthenticated.mockReturnValue(true);
        roleServiceMock.hasRoles.mockReturnValue(true);

        expect(guard.canActivate(routeSnapshotMock as ActivatedRouteSnapshot)).toBe(true);
        expect(navigationServiceMock.navigateToLogin).not.toHaveBeenCalled();
    });

    it('should navigate to login page if user is not authenticated', () => {
        authServiceMock.isAuthenticated.mockReturnValue(false);
        roleServiceMock.hasRoles.mockReturnValue(true);

        expect(guard.canActivate(routeSnapshotMock as ActivatedRouteSnapshot)).toBe(false);
        expect(navigationServiceMock.navigateToLogin).toHaveBeenCalled();
    });

    it('should navigate to login page if user does not have necessary roles', () => {
        authServiceMock.isAuthenticated.mockReturnValue(true);
        roleServiceMock.hasRoles.mockReturnValue(false);

        expect(guard.canActivate(routeSnapshotMock as ActivatedRouteSnapshot)).toBe(false);
        expect(navigationServiceMock.navigateToLogin).toHaveBeenCalled();
    });

    it('should navigate to login page if user is not authenticated and does not have necessary roles', () => {
        authServiceMock.isAuthenticated.mockReturnValue(false);
        roleServiceMock.hasRoles.mockReturnValue(false);

        expect(guard.canActivate(routeSnapshotMock as ActivatedRouteSnapshot)).toBe(false);
        expect(navigationServiceMock.navigateToLogin).toHaveBeenCalled();
    });
});
