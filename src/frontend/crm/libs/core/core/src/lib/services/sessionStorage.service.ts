import { Inject, Injectable } from '@angular/core';
import { SESSION_STORAGE } from './storage-tokens';

@Injectable({ providedIn: 'root' })
export class SessionStorageService {
  private storage: Storage;

  constructor(@Inject(SESSION_STORAGE) private baseStorage: Storage) {
    this.storage = this.initStorage(baseStorage);
  }

  private initStorage(storage: Storage): Storage {
    try {
      storage.setItem('test', 'test');
      storage.removeItem('test');
      return storage;
    } catch (error) {
      console.error('SessionStorage access denied:', error);
      return this.createMemoryStorage();
    }
  }

  private createMemoryStorage(): Storage {
    const storage = new Map<string, string>();
    
    return {
      length: 0,
      clear: () => storage.clear(),
      getItem: (key: string) => storage.get(key) || null,
      setItem: (key: string, value: string) => storage.set(key, value),
      removeItem: (key: string) => storage.delete(key),
      key: (index: number) => Array.from(storage.keys())[index] || null
    } as Storage;
  }

  get<T>(key: string): T | null {
    try {
      const item = this.storage.getItem(key);
      return item ? JSON.parse(item) : null;
    } catch (error) {
      console.error(`Error getting ${key} from SessionStorage:`, error);
      return null;
    }
  }

  set(key: string, value: unknown): void {
    try {
      const serialized = JSON.stringify(value);
      this.storage.setItem(key, serialized);
    } catch (error) {
      console.error(`Error setting ${key} to SessionStorage:`, error);
    }
  }

  remove(key: string): void {
    this.storage.removeItem(key);
  }

  clear(): void {
    this.storage.clear();
  }
}