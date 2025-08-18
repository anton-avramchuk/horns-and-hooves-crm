import { inject, Injectable } from '@angular/core';
import { LocalStorageService } from '@horns-and-hooves/core';
import { BehaviorSubject, Observable } from 'rxjs';
const fullNameKey = 'userNameFull';
@Injectable({
  providedIn: 'root',
})
export class UserFullNameProvider {
  private storageService: LocalStorageService = inject(LocalStorageService);

  private nameValue: BehaviorSubject<string> = new BehaviorSubject<string>(
    this.storageService.get(fullNameKey)
  );

  get name(): Observable<string> {
    return this.nameValue.asObservable();
  }

  public setName(value: string): void {
    if (!value || value.trim().length === 0) {
      this.storageService.remove(fullNameKey);
    } else {
      this.storageService.set(fullNameKey, value);
    }

    this.nameValue.next(value);
  }
}
