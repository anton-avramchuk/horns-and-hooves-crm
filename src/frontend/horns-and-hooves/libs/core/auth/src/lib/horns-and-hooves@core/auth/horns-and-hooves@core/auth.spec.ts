import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HornsAndHoovesCoreAuth } from './auth';

describe('HornsAndHoovesCoreAuth', () => {
  let component: HornsAndHoovesCoreAuth;
  let fixture: ComponentFixture<HornsAndHoovesCoreAuth>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HornsAndHoovesCoreAuth],
    }).compileComponents();

    fixture = TestBed.createComponent(HornsAndHoovesCoreAuth);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
