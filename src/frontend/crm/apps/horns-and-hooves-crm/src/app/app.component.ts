import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NxWelcomeComponent } from './nx-welcome.component';
import { LocalStorageService } from '@crm/core';

@Component({
  imports: [NxWelcomeComponent, RouterModule],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  /**
   *
   */
  constructor(private localStorage: LocalStorageService) {
    
    
  }
  title = 'horns-and-hooves-crm';
}
