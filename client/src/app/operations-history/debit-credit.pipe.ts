import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'debitCredit'
})
export class DebitCreditPipe implements PipeTransform {

  transform(value) {
    debugger;
    return value ? 'Credit' : 'Debit';
}

}
