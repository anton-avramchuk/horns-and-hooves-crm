import { TestBed } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { NavigationService } from './navigation.service';

describe('NavigationService', () => {
    let service: NavigationService;
    let routerMock: jest.Mocked<Router>;
    let activatedRouteMock: Partial<ActivatedRoute>;

    beforeEach(() => {
        routerMock = {
            navigate: jest.fn(),
            url: '/current-url' // Имитация текущего URL
        } as unknown as jest.Mocked<Router>;


        activatedRouteMock = {
            snapshot: {
                queryParamMap: {
                    get: jest.fn().mockReturnValue(null)
                }
            }
        } as unknown as Partial<ActivatedRoute>;

        TestBed.configureTestingModule({
            providers: [
                NavigationService,
                { provide: Router, useValue: routerMock },
                { provide: ActivatedRoute, useValue: activatedRouteMock }
            ]
        });

        service = TestBed.inject(NavigationService);
    });

    it('should navigate to the given URL', () => {
        const url = '/some-path';
        service.navigate(url);
        expect(routerMock.navigate).toHaveBeenCalledWith([url]);
    });

    it('should navigate to home', () => {
        service.navigateToHome();
        expect(routerMock.navigate).toHaveBeenCalledWith(['/']);
    });

    it('should navigate with redirectTo parameter', () => {
        const targetUrl = '/login';
        const currentUrl = routerMock.url;

        service.navigateWithRedirectTo(targetUrl);

        expect(routerMock.navigate).toHaveBeenCalledWith([targetUrl], {
            queryParams: { redirectTo: currentUrl }
        });
    });

    it('should navigate to saved URL if redirectTo is present', () => {
        const redirectTo = '/dashboard';
        ((activatedRouteMock as any).snapshot.queryParamMap.get as jest.Mock).mockReturnValue(redirectTo);
    
        service.redirectToSavedUrl();
    
        expect(routerMock.navigate).toHaveBeenCalledWith([redirectTo]);
      });
    
      it('should navigate to home if redirectTo is not present', () => {
        service.redirectToSavedUrl();
    
        expect(routerMock.navigate).toHaveBeenCalledWith(['/']);
      });
});
