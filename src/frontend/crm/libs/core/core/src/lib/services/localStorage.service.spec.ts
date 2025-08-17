import { TestBed } from '@angular/core/testing';
import { LocalStorageService } from './localStorage.service';
import { LOCAL_STORAGE } from '../core.module';


describe('LocalStorageService', () => {
    let service: LocalStorageService;
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
          LocalStorageService,
          { provide: LOCAL_STORAGE, useValue: mockStorage }
        ]
      });
  
      service = TestBed.inject(LocalStorageService);
      jest.clearAllMocks();
    });
  
    it('should get item', () => {
      const testData = { name: 'Test' };
      mockStorage.getItem.mockReturnValue(JSON.stringify(testData));
      
      const result = service.get('testKey');
      expect(result).toEqual(testData);
      expect(mockStorage.getItem).toHaveBeenCalledWith('testKey');
    });
  
    it('should handle memory storage and log error', () => {
      const error = new Error('Storage blocked');
      const consoleSpy = jest.spyOn(console, 'error').mockImplementation(() => {});
      
      mockStorage.setItem.mockImplementation(() => { throw error; });
      
      service.set('test', 123);
      
      expect(consoleSpy).toHaveBeenCalledWith(
        'Error setting test to LocalStorage:', 
        expect.objectContaining({ message: 'Storage blocked' })
      );
      
      expect(mockStorage.setItem).toHaveBeenCalledWith(
        'test', 
        JSON.stringify(123)
      );
      
      consoleSpy.mockRestore();
    });
  
    it('should return null when getItem fails', () => {
      const consoleSpy = jest.spyOn(console, 'error').mockImplementation(() => {});
      mockStorage.getItem.mockImplementation(() => { throw new Error('Read error'); });
      
      const result = service.get('invalid');
      expect(result).toBeNull();
      expect(consoleSpy).toHaveBeenCalled();
      
      consoleSpy.mockRestore();
    });
  });

  