import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UiLayoutCoreComponent } from './core.component';

describe('UiLayoutCoreComponent', () => {
  let component: UiLayoutCoreComponent;
  let fixture: ComponentFixture<UiLayoutCoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UiLayoutCoreComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UiLayoutCoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
