import { Component, Input } from '@angular/core';
import { IWigetConfiguration } from '../interfaces';

@Component({
  selector: 'lib-widget-container.component',
  imports: [],
  templateUrl: './widget-container.component.html',
  styleUrl: './widget-container.component.scss',
})
export class WidgetContainerComponent {
  @Input() wigets: IWigetConfiguration[] = [];
}
