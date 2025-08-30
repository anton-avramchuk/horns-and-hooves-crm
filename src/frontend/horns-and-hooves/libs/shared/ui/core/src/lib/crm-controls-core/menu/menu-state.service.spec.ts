import { TestBed } from '@angular/core/testing';
import { MenuStateService } from './menu-state.service';
import { MenuChangeEvent } from './menuchangeevent';

describe('MenuService', () => {
    let service: MenuStateService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [MenuStateService]
        });
        service = TestBed.inject(MenuStateService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should emit menu change event', () => {
        const event: MenuChangeEvent = { key: 'someKey', routeEvent: true };
        const spy = jest.spyOn(service['menuSource'], 'next');

        service.onMenuStateChange(event);

        expect(spy).toHaveBeenCalledWith(event);
    });

    it('should emit reset event', () => {
        const spy = jest.spyOn(service['resetSource'], 'next');

        service.reset();

        expect(spy).toHaveBeenCalledWith(true);
    });

    // Другие тесты по мере необходимости
});
