import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'thainumber'
})
export class ThaiNumberPipe implements PipeTransform {
  numbers: Array<string> = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0'];

  thaiNumbers: Array<string> = [
    '๑',
    '๒',
    '๓',
    '๔',
    '๕',
    '๖',
    '๗',
    '๘',
    '๙',
    '๐'
  ];

  replaceAll(str, find, replace) {
    return str.toString().replace(new RegExp(find, 'g'), replace);
  }

  transform(value: string): string {
    let result = value;
    for (let i = 0; i < this.numbers.length; i++) {
      result = this.replaceAll(result, this.numbers[i], this.thaiNumbers[i]);
    }
    return result;
  }

  parse(value: string): string {
    if (!value) {
      return '';
    }

    let result = value;
    for (let i = 0; i < this.numbers.length; i++) {
      result = this.replaceAll(result, this.thaiNumbers[i], this.numbers[i]);
    }
    return result;
  }
}
