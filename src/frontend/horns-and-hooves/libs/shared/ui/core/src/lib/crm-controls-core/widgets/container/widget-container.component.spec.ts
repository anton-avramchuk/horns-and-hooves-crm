import { Component } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { WidgetContainerComponent } from './widget-container.component';
import {
  AUTH_SERVICE,
  CLAIMS_PERMISSION_SERVICE,
  IAuthService,
  IClaimsPermissionService,
  ILoginResponse,
  IRolePermissionService,
  ROLE_PERMISSION_SERVICE,
} from '@horns-and-hooves/core-auth';
import { Observable } from 'rxjs';

// 🔹 Тестовый мок-компонент для рендера
@Component({
  selector: 'test-widget',
  template: `<span>Test Widget</span>`,
})
class TestWidgetComponent {}

// 🔹 Моки сервисов
class MockAuthService implements IAuthService {
  login<TResponse extends ILoginResponse>(userName: string, password: string): Observable<TResponse> {
    throw new Error('Method not implemented.');
  }
  logOff(): void {
    throw new Error('Method not implemented.');
  }
  authenticated = false;
  isAuthenticated() {
    return this.authenticated;
  }
}
class MockRolePermissionService implements IRolePermissionService {
  roles: string[] = [];
  hasRoles(required: string[]): boolean {
    return required.every((r) => this.roles.includes(r));
  }
}
class MockClaimsPermissionService implements IClaimsPermissionService {
  claims: string[] = [];
  hasClaims(required: string[]): boolean {
    return required.every((c) => this.claims.includes(c));
  }
}

describe('WidgetContainerComponent', () => {
  let fixture: ComponentFixture<WidgetContainerComponent>;
  let component: WidgetContainerComponent;
  let authService: MockAuthService;
  let rolesService: MockRolePermissionService;
  let claimsService: MockClaimsPermissionService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WidgetContainerComponent, TestWidgetComponent],
      providers: [
        { provide: AUTH_SERVICE, useClass: MockAuthService },
        { provide: ROLE_PERMISSION_SERVICE, useClass: MockRolePermissionService },
        { provide: CLAIMS_PERMISSION_SERVICE, useClass: MockClaimsPermissionService },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(WidgetContainerComponent);
    component = fixture.componentInstance;
    authService = TestBed.inject(AUTH_SERVICE) as unknown as MockAuthService;
    rolesService = TestBed.inject(ROLE_PERMISSION_SERVICE) as unknown as MockRolePermissionService;
    claimsService = TestBed.inject(CLAIMS_PERMISSION_SERVICE) as unknown as MockClaimsPermissionService;
  });

  function detectAndQuery(): HTMLElement {
    fixture.detectChanges();
    return fixture.nativeElement as HTMLElement;
  }

  it('renders nothing if widgets empty', () => {
    component.widgets = [];
    const el = detectAndQuery();
    expect(el.textContent?.trim()).toBe('');
  });

  it('hides widget if not authenticated and allowForUnauthorized = false', () => {
    component.widgets = [
      { component: TestWidgetComponent, allowForUnauthorized: false },
    ];
    authService.authenticated = false;
    const el = detectAndQuery();
    expect(el.textContent).not.toContain('Test Widget');
  });

  it('shows widget if not authenticated but allowForUnauthorized = true', () => {
    component.widgets = [
      { component: TestWidgetComponent, allowForUnauthorized: true },
    ];
    authService.authenticated = false;
    const el = detectAndQuery();
    expect(el.textContent).toContain('Test Widget');
  });

  it('hides widget if roles do not match', () => {
    component.widgets = [
      { component: TestWidgetComponent, roles: ['admin'] },
    ];
    authService.authenticated = true;
    rolesService.roles = []; // нет admin
    const el = detectAndQuery();

    expect(component.visibleWidgets).toHaveLength(0);

    expect(el.textContent).not.toContain('Test Widget');
  });

  it('shows widget if user has required roles', () => {
    component.widgets = [
      { component: TestWidgetComponent, roles: ['admin'] },
    ];
    authService.authenticated = true;
    rolesService.roles = ['admin'];
    const el = detectAndQuery();
    expect(el.textContent).toContain('Test Widget');
  });

  it('hides widget if claims do not match', () => {
    component.widgets = [
      { component: TestWidgetComponent, claims: ['claim1'] },
    ];
    authService.authenticated = true;
    claimsService.claims = []; // нет claim1
    const el = detectAndQuery();
    expect(el.textContent).not.toContain('Test Widget');
  });

  it('shows widget if user has required claims', () => {
    component.widgets = [
      { component: TestWidgetComponent, claims: ['claim1'] },
    ];
    authService.authenticated = true;
    claimsService.claims = ['claim1'];
    const el = detectAndQuery();
    expect(el.textContent).toContain('Test Widget');
  });

  it('sorts widgets by order ascending', () => {
    component.widgets = [
      { component: TestWidgetComponent, order: 2 },
      { component: TestWidgetComponent, order: 1 },
    ];
    authService.authenticated = true;
    const el = detectAndQuery();
    const spans = el.querySelectorAll('span');
    expect(spans[0].textContent).toBe('Test Widget'); // порядок 1
  });

  it('applies cssClass and tooltip', () => {
    component.widgets = [
      { component: TestWidgetComponent, cssClass: 'custom-class', tooltip: 'tip' },
    ];
    authService.authenticated = true;
    const el = detectAndQuery();
    const div = el.querySelector('div.custom-class')!;
    expect(div).toBeTruthy();
    expect(div.getAttribute('title')).toBe('tip');
  });
});
