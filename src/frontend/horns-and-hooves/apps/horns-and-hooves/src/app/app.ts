import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NxWelcome } from './nx-welcome';
import{ HornsAndHoovesCoreAuthModule} from '@horns-and-hooves/core-auth';
import { HornsAndHoovesCoreModule } from '@horns-and-hooves/core';
import { HornsAndHoovesCoreLayoutModule } from '@horns-and-hooves/ui-controls-core';

@Component({
  imports: [
    NxWelcome,
    RouterModule,
    HornsAndHoovesCoreLayoutModule,
    HornsAndHoovesLayoutModule,
    HornsAndHoovesCoreModule,
    HornsAndHoovesCoreAuthModule
  ],
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  protected title = 'horns-and-hooves';
}
