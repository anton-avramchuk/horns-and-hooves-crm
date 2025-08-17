import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

// Функция, создающая валидатор
export function hexColorValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        const value = control.value;
        // Регулярное выражение для проверки формата hex-цвета

        const isValidHexColor = /^#([0-9A-F]{3}){1,2}$/i.test(value);
        return isValidHexColor ? null : { invalidHexColor: { value } };
    };
}