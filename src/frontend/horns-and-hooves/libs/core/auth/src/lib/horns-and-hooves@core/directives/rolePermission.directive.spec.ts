import { TestBed, ComponentFixture } from '@angular/core/testing';
import { CrmRoleHasPermissionDirective } from './rolePermission.directive';
import { Component, ViewChild } from '@angular/core';
import { AUTH_SERVICE, ROLE_PERMISSION_SERVICE } from '../crm-core-auth.module';

@Component({
    template: `
    <div *crmHasRolePermission="requiredPermissions">Content</div>
  `
})
class TestComponent {
    @ViewChild(CrmRoleHasPermissionDirective, { static: true }) directive!: CrmRoleHasPermissionDirective;
    requiredPermissions: string[] = [];
}

describe('CrmRoleHasPermissionDirective', () => {
    let authServiceMock: any;
    let roleServiceMock: any;
    let fixture: ComponentFixture<TestComponent>;
    let testComponent: TestComponent;

    beforeEach(() => {
        authServiceMock = {
            isAuthenticated: jest.fn()
        };

        roleServiceMock = {
            hasRoles: jest.fn()
        };

        TestBed.configureTestingModule({
            declarations: [CrmRoleHasPermissionDirective, TestComponent], // Объявляем директиву в тестовом модуле
            providers: [
                { provide: AUTH_SERVICE, useValue: authServiceMock },
                { provide: ROLE_PERMISSION_SERVICE, useValue: roleServiceMock }
            ]
        });

        fixture = TestBed.createComponent(TestComponent);
        testComponent = fixture.componentInstance;
    });

    it('should create an instance', () => {
        expect(testComponent).toBeTruthy();
    });

    it('should render content if user has necessary roles', () => {
        authServiceMock.isAuthenticated.mockReturnValue(true);
        roleServiceMock.hasRoles.mockReturnValue(true);
        testComponent.requiredPermissions = ['role1'];

        fixture.detectChanges();

        const element: HTMLElement = fixture.nativeElement.querySelector('div');
        expect(element.innerHTML.trim()).toBe('Content');
    });

    it('should not render content if user does not have necessary roles', () => {
        authServiceMock.isAuthenticated.mockReturnValue(true);
        roleServiceMock.hasRoles.mockReturnValue(false);
        testComponent.requiredPermissions = ['role1'];

        fixture.detectChanges();

        const element: HTMLElement = fixture.nativeElement.querySelector('div');
        expect(element).toBeFalsy();
    });

    it('should not render content if user is not authenticated', () => {
        authServiceMock.isAuthenticated.mockReturnValue(false);
        roleServiceMock.hasRoles.mockReturnValue(true);
        testComponent.requiredPermissions = ['role1'];

        fixture.detectChanges();

        const element: HTMLElement = fixture.nativeElement.querySelector('div');
        expect(element).toBeFalsy();
    });
});
