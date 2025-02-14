import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'shortenId'
})
export class ShortenIdPipe implements PipeTransform {
  transform(value: string): string {
    return value ? value.substring(0, 8).toUpperCase() + '...' : '';
  }
}