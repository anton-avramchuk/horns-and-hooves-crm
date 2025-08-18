import { TestBed } from '@angular/core/testing';
import { AuthNavigationService } from './auth.navigation.service';
import { AUTH_LOGIN_PATH } from '../crm-core-auth.module';
import { NavigationService } from '@horns-and-hooves/core';

describe('AuthNavigationService', () => {
    let service: AuthNavigationService;
    let navigationServiceMock: jest.Mocked<NavigationService>;
    const mockLoginPath = '/login';

    beforeEach(() => {
        navigationServiceMock = {
            navigate: jest.fn(),
            navigateToHome: jest.fn(),
            // другие методы, если есть
        } as unknown as jest.Mocked<NavigationService>;


        TestBed.configureTestingModule({
            providers: [
                AuthNavigationService,
                { provide: NavigationService, useValue: navigationServiceMock },
                { provide: AUTH_LOGIN_PATH, useValue: mockLoginPath }
            ]
        });
        service = TestBed.inject(AuthNavigationService);
    });

    it('should navigate to login', () => {
        service.navigateToLogin();
        expect(navigationServiceMock.navigate).toHaveBeenCalledWith(mockLoginPath);
    });

    it('should navigate to home', () => {
        service.navigateToHome();
        expect(navigationServiceMock.navigateToHome).toHaveBeenCalled();
    });
});
