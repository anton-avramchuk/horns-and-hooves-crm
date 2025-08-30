import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CrmControls } from './crm-controls';

describe('CrmControls', () => {
  let component: CrmControls;
  let fixture: ComponentFixture<CrmControls>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CrmControls],
    }).compileComponents();

    fixture = TestBed.createComponent(CrmControls);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
