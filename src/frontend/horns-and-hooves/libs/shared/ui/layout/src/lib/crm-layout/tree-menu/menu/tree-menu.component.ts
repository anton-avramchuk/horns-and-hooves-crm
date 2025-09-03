import { Component } from '@angular/core';
import { TreeMenuItemComponent } from '../menu-item/tree-menu-item.component';

@Component({
  selector: 'horns-and-hooves-tree-menu',
  imports: [TreeMenuItemComponent],
  templateUrl: './tree-menu.component.html',
  styleUrl: './tree-menu.component.scss',
})
export class TreeMenuComponent {}
