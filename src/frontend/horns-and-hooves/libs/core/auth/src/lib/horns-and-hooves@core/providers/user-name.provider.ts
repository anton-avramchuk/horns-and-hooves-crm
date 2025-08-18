import { inject, Injectable } from '@angular/core';
import { LocalStorageService } from '@horns-and-hooves/core';
import { BehaviorSubject, Observable } from 'rxjs';
const userNameKey = 'userName';
@Injectable({
  providedIn: 'root',
})
export class UserNameProvider {
  private storageService: LocalStorageService = inject(LocalStorageService);

  private userNameValue: BehaviorSubject<string> = new BehaviorSubject<string>(
    this.storageService.get(userNameKey)
  );

  get userName(): Observable<string> {
    return this.userNameValue.asObservable();
  }


  public setUserName(value: string): void {
    if (!value || value.trim().length === 0) {
      this.storageService.remove(userNameKey);
    } else {
      this.storageService.set(userNameKey, value);
    }

    this.userNameValue.next(value);
  }
}
