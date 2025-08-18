import { Inject, Injectable } from '@angular/core';
import { APP_STORAGE_KEY, LOCAL_STORAGE } from '../horns-and-hooves-core.module';
@Injectable({ providedIn: 'root' })
export class LocalStorageService {
    private storageKey: string;
    private localStorage: Storage;
    /**
     *
     */
    // eslint-disable-next-line @angular-eslint/prefer-inject
    constructor(@Inject(APP_STORAGE_KEY) key: string, @Inject(LOCAL_STORAGE) localStorage: any) {
        this.storageKey = key;
        if (!this.storageKey.endsWith('.')) {
            this.storageKey = this.storageKey + '.';
        }
        this.localStorage = localStorage as Storage;
    }

    private getKey(key: string): string {
        return this.storageKey + key;
    }

    set(key: string, value: any): void {
        this.localStorage.setItem(this.getKey(key), JSON.stringify(value));
    }

    get(key: string): any {
        const value = this.localStorage.getItem(this.getKey(key)) as string;
        try {
            return JSON.parse(value);
        } catch {
            return value;
        }
    }

    remove(key: string): void {
        return this.localStorage.removeItem(this.getKey(key));
    }
}
