import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocalStorageService } from './services/localStorage.service';
import { SessionStorageService } from './services/sessionStorage.service';

@NgModule({
  imports: [CommonModule],
  providers: [
    LocalStorageService,
    SessionStorageService
  ]
})
export class CrmBrowserModule {}
