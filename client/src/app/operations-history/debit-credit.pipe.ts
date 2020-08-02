import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'debitCredit'
})
export class DebitCreditPipe implements PipeTransform {

  transform(value) {
    return value ? 'Credit' : 'Debit';
}

}
