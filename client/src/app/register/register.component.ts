import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { DataService } from '../shared/services/data.service';
import { Register } from './register.model';
import { UserService } from '../shared/services/user.service';
import { UserAddress } from './userAddress.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registrationForm: FormGroup;
  verificationForm: FormGroup;
  verificationLoading = false;
  registrationLoading = false;
  registrationSubmitted = false;
  verificationSubmitted = false;
  email: string;
  isVisibleVerificationForm: boolean = true;
  isVisibleRegistrationForm: boolean = false;

  userToRegister = <Register>{};

  constructor(private router: Router, private dataService: DataService, private userService: UserService) { }

  ngOnInit(): void {

    this.registrationForm = new FormGroup({
      'password': new FormControl('', [
        Validators.required,
        Validators.minLength(6)]),
      'firstName': new FormControl('', [
        Validators.required]),
        'userId': new FormControl('', [
          Validators.required]),
      'lastName': new FormControl('', [
        Validators.required]),
      'street': new FormControl('', [
        Validators.required]),
        'city': new FormControl('', [
          Validators.required]),
      'houseNumber': new FormControl('', [
        Validators.required]),
      'postCode': new FormControl('', [
        Validators.required]),
      'verificationCode': new FormControl('', [
        Validators.required,
        Validators.minLength(4)]),
    });
    this.verificationForm = new FormGroup({
      'email': new FormControl('', [
        Validators.required,
        Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")
      ])
    });
  }

  get registrationFormControls() { return this.registrationForm.controls; }
  get verificationFormControls() { return this.verificationForm.controls; }
  get verifiedEmail() { return this.verificationForm.get('email'); }

  onVerificationSubmit() {
    this.verificationSubmitted = true;
    if (this.verificationForm.invalid) {
      return;
    }
    this.verificationLoading = true;
    this.email = this.verificationForm.value;
  
    this.userService.verifyEmail(this.email)
      .subscribe(
        Response => {
          alert("An Email with a verification code will be sent to you shortly");
          this.verificationLoading = false;
          this.showRegistrationForm();
        },
        error => {
          alert(error)
          this.verificationLoading = false;
        });
  }

  onRegisterSubmit() {
    this.registrationSubmitted = true;
    if (this.registrationForm.invalid) {
      return;
    }
    this.registrationLoading = true;
    this.userToRegister.firstName = this.registrationFormControls.firstName.value;

    this.userToRegister.lastName = this.registrationFormControls.lastName.value;
    this.userToRegister.password = this.registrationFormControls.password.value;

    this.userToRegister.userId = this.registrationFormControls.userId.value;
    this.userToRegister.verificationCode = this.registrationFormControls.verificationCode.value;
    //this.userToRegister = this.registrationForm.value;
    let address = <UserAddress>{};
    address.city = this.registrationFormControls.city.value;
    address.houseNumber = Number(this.registrationFormControls.houseNumber.value);
    address.street = this.registrationFormControls.street.value;
    address.postCode = Number(this.registrationFormControls.postCode.value);
    this.userToRegister.email = this.verificationFormControls.email.value;
    this.userToRegister.address =address;
    this.userService.register(this.userToRegister)
      .subscribe(
        result => {
          alert("Registration completed. please login with your email and password.")
          this.router.navigate(['/login']);
        },
        error => {
          alert(error)
          this.registrationLoading = false;
        });
  }

  showVerificationForm() {
    this.isVisibleRegistrationForm = false;
    this.isVisibleVerificationForm = true;

  }

  showRegistrationForm() {
    this.isVisibleRegistrationForm = true;
    this.isVisibleVerificationForm = false;
  }

}




