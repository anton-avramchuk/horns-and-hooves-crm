import { Component } from '@angular/core';
import { TreeMenuComponent } from '../tree-menu/menu/tree-menu.component';

@Component({
  selector: 'horns-and-hooves-sidebar',
  imports: [TreeMenuComponent],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
})
export class SidebarComponent {}
