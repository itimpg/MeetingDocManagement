import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'thaidate'
})
export class ThaiDatePipe implements PipeTransform {
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

  transform(value: Date, args?: any): any {
    if (!value) {
      return '';
    }

    const date = new Date(value);
    let result = `${date.getDate()}/${date.getMonth() +
      1}/${date.getFullYear() + 543} ${date.getHours()}:${date.getMinutes()}`;

    for (let i = 0; i < this.numbers.length; i++) {
      result = this.replaceAll(result, this.numbers[i], this.thaiNumbers[i]);
    }

    return result;
  }

  replaceAll(str, find, replace) {
    return str.toString().replace(new RegExp(find, 'g'), replace);
  }
}
