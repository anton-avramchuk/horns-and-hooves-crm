import { Component, TemplateRef, ViewChild } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { IsAuthDirective } from './isAuth.directive';
import { AUTH_SERVICE } from '../crm-core-auth.module';
import { CommonModule } from '@angular/common';

class MockAuthService {
  isAuthenticatedValue = false;
  isAuthenticated() {
    return this.isAuthenticatedValue;
  }
}

@Component({
  template: `
    <ng-template #tpl>
      <span>Authenticated!</span>
    </ng-template>
    <ng-container
      *hornsAndHoovesAuthIsAuthDirective
      [ngTemplateOutlet]="tpl"
    ></ng-container>
  `,
  imports: [IsAuthDirective, CommonModule],
})
class TestHostComponent {
  @ViewChild('tpl', { static: true }) tpl!: TemplateRef<any>;
}

describe('IsAuthDirective', () => {
  let fixture: ComponentFixture<TestHostComponent>;
  let mockAuthService: MockAuthService;

  beforeEach(() => {
    mockAuthService = new MockAuthService();

    TestBed.configureTestingModule({
      imports: [TestHostComponent],
      providers: [{ provide: AUTH_SERVICE, useValue: mockAuthService }],
    });

    fixture = TestBed.createComponent(TestHostComponent);
  });


  it('should render template when authenticated', () => {
    mockAuthService.isAuthenticatedValue = true;
    fixture.detectChanges();
    expect(fixture.nativeElement.textContent).toContain('Authenticated!');
  });

  it('should not render template when not authenticated', () => {
    mockAuthService.isAuthenticatedValue = false;
    fixture.detectChanges();
    expect(fixture.nativeElement.textContent).not.toContain('Authenticated!');
  });
});
