import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TreeMenuItemComponent } from './tree-menu-item.component';

describe('TreeMenuItemComponent', () => {
  let component: TreeMenuItemComponent;
  let fixture: ComponentFixture<TreeMenuItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TreeMenuItemComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TreeMenuItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
