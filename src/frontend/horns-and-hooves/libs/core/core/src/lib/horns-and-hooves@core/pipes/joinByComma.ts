import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'joinByComma'
})
export class JoinByCommaPipe implements PipeTransform {
  transform(value: any[], separator = ', '): string {
    if (!Array.isArray(value)) {
      return '';
    }
    return value.join(separator);
  }
}
