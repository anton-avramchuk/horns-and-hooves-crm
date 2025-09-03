import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FixedMenuLayoutComponent } from './fixed-menu-layout.component';

describe('FixedMenuLayoutComponent', () => {
  let component: FixedMenuLayoutComponent;
  let fixture: ComponentFixture<FixedMenuLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FixedMenuLayoutComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(FixedMenuLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
