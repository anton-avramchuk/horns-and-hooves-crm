import { inject, Injectable } from '@angular/core';
import { LocalStorageService } from '@horns-and-hooves/core';
const tokenKey = 'userToken';
@Injectable({
  providedIn: 'root',
})
export class TokenProvider {
  private storageService: LocalStorageService = inject(LocalStorageService);

  public getToken(): string {
    return this.storageService.get(tokenKey);
  }

  constructor() {}

  public setToken(value: string): void {
    if (!value || value.trim().length === 0) {
      this.storageService.remove(tokenKey);
    } else {
      this.storageService.set(tokenKey, value);
    }
  }
}
