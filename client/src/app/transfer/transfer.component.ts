import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Transfer } from './transfer.model';
import { DataService } from '../shared/services/data.service';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-transfer',
  templateUrl: './transfer.component.html',
  styleUrls: ['./transfer.component.css']
})
export class TransferComponent implements OnInit {

  transferForm: FormGroup;
  loading = false;
  submitted = false;
  transfer: Transfer;

  constructor(private dataService: DataService,private authService: AuthService) { }

  ngOnInit(): void {
    this.transferForm = new FormGroup({
      'amount': new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.max(1000000)
      ]),
      'destAccountId': new FormControl('', [
        Validators.required,
      ])
    });

  }

  get formControls() { return this.transferForm.controls; }
  onSubmit() {
    this.submitted = true;
    if (this.transferForm.invalid) {
      return;
    }
    this.transfer = this.transferForm.value;
    // this.transfer.destAccountId=this.transferForm.value.destAccountId );
    this.transfer.srcAccountId=this.authService.getUserAccountId();
    this.loading = true;
    this.dataService.commitTransaction(this.transfer)
      .subscribe(
        result => {
         alert("your request has been accepted!");
         this.loading = false;
        },
        error => {
          alert(error);
          this.loading = false;
        });
  }
}
