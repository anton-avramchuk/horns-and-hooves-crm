import { TestBed } from '@angular/core/testing';
import { TokenAuthService } from './token.auth.service';
import { LocalStorageService } from '@horns-and-hooves/core';

describe('TokenAuthService', () => {
    let service: TokenAuthService;
    let localStorageService: jest.Mocked<LocalStorageService>;

    beforeEach(() => {
        const localStorageSpy = {
            get: jest.fn(),
            set: jest.fn(),
            remove: jest.fn()
        };

        TestBed.configureTestingModule({
            providers: [
                TokenAuthService,
                { provide: LocalStorageService, useValue: localStorageSpy }
            ]
        });

        service = TestBed.inject(TokenAuthService);
        localStorageService = TestBed.inject(LocalStorageService) as jest.Mocked<LocalStorageService>;
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should return true if user is authenticated', () => {
        localStorageService.get.mockReturnValue('someToken');
        const isAuthenticated = service.isAuthenticated();
        expect(isAuthenticated).toBe(true);
    });

    it('should return false if user is not authenticated', () => {
        localStorageService.get.mockReturnValue(null);
        const isAuthenticated = service.isAuthenticated();
        expect(isAuthenticated).toBe(false);
    });

    it('should remove token, roles, and claims on logOff', () => {
        service.logOff();
        expect(localStorageService.remove).toHaveBeenCalledTimes(3);
        expect(localStorageService.remove).toHaveBeenCalledWith('userToken');
        expect(localStorageService.remove).toHaveBeenCalledWith('roles');
        expect(localStorageService.remove).toHaveBeenCalledWith('claims');
    });

    // Добавьте еще тесты для других методов при необходимости
});
