import { TestBed, ComponentFixture } from '@angular/core/testing';
import { CrmClaimsHasPermissionDirective } from './claimsPermission.directive';
import { Component, ViewChild } from '@angular/core';
import { AUTH_SERVICE, CLAIMS_PERMISSION_SERVICE } from '../crm-core-auth.module';

@Component({
    template: `
    <div *crmHasClaimsPermission="requiredPermissions">Content</div>
  `
})
class TestComponent {
    @ViewChild(CrmClaimsHasPermissionDirective, { static: true }) directive!: CrmClaimsHasPermissionDirective;
    requiredPermissions: string[] = [];
}

describe('CrmClaimsHasPermissionDirective', () => {
    let authServiceMock: any;
    let claimsServiceMock: any;
    let fixture: ComponentFixture<TestComponent>;
    let testComponent: TestComponent;

    beforeEach(() => {
        authServiceMock = {
            isAuthenticated: jest.fn()
        };

        claimsServiceMock = {
            hasClaims: jest.fn()
        };

        TestBed.configureTestingModule({
            declarations: [CrmClaimsHasPermissionDirective, TestComponent],
            providers: [
                { provide: AUTH_SERVICE, useValue: authServiceMock },
                { provide: CLAIMS_PERMISSION_SERVICE, useValue: claimsServiceMock }
            ]
        });

        fixture = TestBed.createComponent(TestComponent);
        testComponent = fixture.componentInstance;
    });

    it('should create an instance', () => {
        expect(testComponent).toBeTruthy();
    });

    it('should render content if user has necessary claims', () => {
        authServiceMock.isAuthenticated.mockReturnValue(true);
        claimsServiceMock.hasClaims.mockReturnValue(true);
        testComponent.requiredPermissions = ['permission1'];

        fixture.detectChanges();

        const element: HTMLElement = fixture.nativeElement.querySelector('div');
        expect(element.innerHTML.trim()).toBe('Content');
    });

    it('should not render content if user does not have necessary claims', () => {
        authServiceMock.isAuthenticated.mockReturnValue(true);
        claimsServiceMock.hasClaims.mockReturnValue(false);
        testComponent.requiredPermissions = ['permission1'];

        fixture.detectChanges();

        const element: HTMLElement = fixture.nativeElement.querySelector('div');
        expect(element).toBeFalsy();
    });

    it('should not render content if user is not authenticated', () => {
        authServiceMock.isAuthenticated.mockReturnValue(false);
        claimsServiceMock.hasClaims.mockReturnValue(true);
        testComponent.requiredPermissions = ['permission1'];

        fixture.detectChanges();

        const element: HTMLElement = fixture.nativeElement.querySelector('div');
        expect(element).toBeFalsy();
    });
});
