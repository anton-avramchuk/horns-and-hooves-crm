import { TestBed } from '@angular/core/testing';
import { LocalStorageService } from './localStorage.service';
import { APP_STORAGE_KEY, LOCAL_STORAGE } from './../crm-core.module';

describe('LocalStorageService', () => {
    let service: LocalStorageService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [
                LocalStorageService,
                { provide: APP_STORAGE_KEY, useValue: 'test_key' },
                { provide: LOCAL_STORAGE, useValue: localStorage }
            ]
        });
        service = TestBed.inject(LocalStorageService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should set and get value from local storage', () => {
        const testKey = 'test';
        const testValue = { value: 'test' };

        service.set(testKey, testValue);
        const retrievedValue = service.get(testKey);

        expect(retrievedValue).toEqual(testValue);
    });

    it('should remove value from local storage', () => {
        const testKey = 'test';
        const testValue = { value: 'test' };

        service.set(testKey, testValue);
        service.remove(testKey);
        const retrievedValue = service.get(testKey);

        expect(retrievedValue).toBeNull();
    });
});