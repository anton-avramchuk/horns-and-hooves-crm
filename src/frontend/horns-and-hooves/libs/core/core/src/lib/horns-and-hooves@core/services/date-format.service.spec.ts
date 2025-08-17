import { TestBed } from '@angular/core/testing';
import { DateFormatService } from './date-format.service';
import { DatePipe } from '@angular/common';

describe('DateFormatService', () => {
    let service: DateFormatService;
    let datePipe: DatePipe;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [DateFormatService, DatePipe]
        });
        service = TestBed.inject(DateFormatService);
        datePipe = TestBed.inject(DatePipe);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should format date as dd.MM.yyyy', () => {
        const date = new Date(2024, 7, 22); // 22 August 2024
        const formattedDate = service.formatDate(date);
        expect(formattedDate).toBe('22.08.2024');
    });

    it('should format date as dd.MM.yyyy HH:mm:ss', () => {
        const date = new Date(2024, 7, 22, 15, 30, 45); // 22 August 2024, 15:30:45
        const formattedFullDate = service.formatFullDate(date);
        expect(formattedFullDate).toBe('22.08.2024 15:30:45');
    });

    it('should format date with a custom format', () => {
        const date = new Date(2024, 7, 22); // 22 August 2024
        const customFormat = 'yyyy-MM-dd';
        const customFormattedDate = service.format(date, customFormat);
        expect(customFormattedDate).toBe('2024-08-22');
    });

    it('should handle null date gracefully', () => {
        const formattedDate = service.formatDate(null);
        expect(formattedDate).toBeNull();
    });

    it('should handle null date or format gracefully', () => {
        const formattedDate = service.format(null, null);
        expect(formattedDate).toBeNull();
    });
});
