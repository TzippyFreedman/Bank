import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-transfer',
  templateUrl: './transfer.component.html',
  styleUrls: ['./transfer.component.css']
})
export class TransferComponent implements OnInit {

  transferForm: FormGroup;
  loading = false;
  submitted = false;

  constructor() { }

  ngOnInit(): void {
    this.transferForm = new FormGroup({
      'amount': new FormControl('', [
        Validators.required,
      ])
    });

  }
}
