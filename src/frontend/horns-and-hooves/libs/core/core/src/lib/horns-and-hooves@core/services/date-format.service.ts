import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';

@Injectable({
    providedIn: 'root'
})
export class DateFormatService {

    // eslint-disable-next-line @angular-eslint/prefer-inject
    constructor(private datePipe: DatePipe) { }

    /**
     * Форматирует дату в формат dd.MM.yyyy
     * @param date - Дата для форматирования
     * @returns Отформатированная строка
     */
    formatDate(date: Date | null): string | null {
        return date ? this.datePipe.transform(date, 'dd.MM.yyyy') : null;
    }

    /**
     * Форматирует дату в формат dd.MM.yyyy HH:mm:ss
     * @param date - Дата для форматирования
     * @returns Отформатированная строка
     */
    formatFullDate(date: Date | null): string | null {
        return date ? this.datePipe.transform(date, 'dd.MM.yyyy HH:mm:ss') : null;
    }

    /**
     * Общий метод для форматирования даты по указанному формату
     * @param date - Дата для форматирования
     * @param format - Формат для использования
     * @returns Отформатированная строка
     */
    format(date: Date | null, format: string | null): string | null {
        return (date && format) ? this.datePipe.transform(date, format) : null;
    }
}
