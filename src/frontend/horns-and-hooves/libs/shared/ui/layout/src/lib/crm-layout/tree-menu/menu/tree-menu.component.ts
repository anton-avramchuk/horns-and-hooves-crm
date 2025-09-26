import { Component, inject, Input, OnInit } from '@angular/core';
import { TreeMenuItemComponent } from '../menu-item/tree-menu-item.component';
import { IMenuItem, MenuService } from '@horns-and-hooves/ui-controls-core';

@Component({
  selector: 'horns-and-hooves-tree-menu',
  imports: [TreeMenuItemComponent],
  templateUrl: './tree-menu.component.html',
  styleUrl: './tree-menu.component.scss',
})
export class TreeMenuComponent implements OnInit {
  private readonly menuService = inject(MenuService);
  menuItems: IMenuItem[] = [];

  @Input() menuKey = '';
 

  ngOnInit(): void {
    if (!this.menuKey || this.menuKey.length === 0) {
      throw new Error(`Invalid menu key=${this.menuKey}.`);
    }

    this.menuItems = this.menuService.getMenu(this.menuKey);
  }
}
