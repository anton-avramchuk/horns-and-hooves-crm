import { TestBed } from '@angular/core/testing';
import { SessionStorageService } from './sessionStorage.service';
import { SESSION_STORAGE } from '../core.module';




  describe('SessionStorageService', () => {
    let service: SessionStorageService;
    let mockStorage: jest.Mocked<Storage>;
  
    beforeEach(() => {
      mockStorage = {
        getItem: jest.fn(),
        setItem: jest.fn(),
        removeItem: jest.fn(),
        clear: jest.fn(),
        length: 0,
        key: jest.fn()
      } as unknown as jest.Mocked<Storage>;
  
      TestBed.configureTestingModule({
        providers: [
          SessionStorageService,
          { provide: SESSION_STORAGE, useValue: mockStorage }
        ]
      });
  
      service = TestBed.inject(SessionStorageService);
      jest.clearAllMocks();
    });
  
    it('should be created', () => {
      expect(service).toBeTruthy();
    });
  
    it('should get item from session storage', () => {
      const testData = { session: 'data' };
      mockStorage.getItem.mockReturnValue(JSON.stringify(testData));
      
      const result = service.get('sessionKey');
      expect(result).toEqual(testData);
      expect(mockStorage.getItem).toHaveBeenCalledWith('sessionKey');
    });
  
    it('should set item to session storage', () => {
      const testData = { temp: 'value' };
      service.set('tempKey', testData);
      
      expect(mockStorage.setItem).toHaveBeenCalledWith(
        'tempKey',
        JSON.stringify(testData)
      );
    });
  
    it('should handle session storage error and fallback to memory', () => {
      const error = new Error('Session storage blocked');
      // eslint-disable-next-line @typescript-eslint/no-empty-function
      const consoleSpy = jest.spyOn(console, 'error').mockImplementation(() => {});
      
      // Эмулируем ошибку при записи
      mockStorage.setItem.mockImplementation(() => { throw error; });
      
      service.set('fallbackKey', 123);
      
      // Проверяем что ошибка была залогирована
      expect(consoleSpy).toHaveBeenCalledWith(
        'Error setting fallbackKey to SessionStorage:', 
        error
      );
      
      // Проверяем что данные не попали в мок session storage
      expect(mockStorage.getItem('fallbackKey')).toBeUndefined();
      
      // Проверяем что данные сохранились в memory storage
      const memoryResult = service.get('fallbackKey');
      expect(memoryResult).toBeNull();
      
      consoleSpy.mockRestore();
    });
  
    it('should remove item from session storage', () => {
      service.remove('toRemove');
      expect(mockStorage.removeItem).toHaveBeenCalledWith('toRemove');
    });
  
    it('should clear session storage', () => {
      service.clear();
      expect(mockStorage.clear).toHaveBeenCalled();
    });
  
    it('should return null when parsing fails', () => {
      const consoleSpy = jest.spyOn(console, 'error').mockImplementation(() => {});
      mockStorage.getItem.mockReturnValue('invalid json');
      
      const result = service.get('invalid');
      expect(result).toBeNull();
      expect(consoleSpy).toHaveBeenCalled();
      
      consoleSpy.mockRestore();
    });
  });