import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { IUser } from '../shared/models/IUser';
import { DataService } from '../shared/services/data.service';
import { IRegister } from '../shared/models/IRegister';



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
  isVisibleVertificationForm: boolean = true;
  isVisibleRegistrationForm: boolean = false;


  userToRegister = <IRegister>{};

  constructor(
    private router: Router,
    private dataService: DataService
  ) {

    // redirect to home if already logged in
    // if (this.authenticationService.currentUserValue) {
    //     this.router.navigate(['/']);
    // }
  }



  ngOnInit(): void {

    this.registrationForm = new FormGroup({
      'password': new FormControl('', [
        Validators.required,
        Validators.minLength(6)]),
      'firstName': new FormControl('', [
        Validators.required]),
      'lastName': new FormControl('', [
        Validators.required]),
        'verificationCode': new FormControl('', [
          Validators.required]),
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

  onVerificationSubmit() {
    this.verificationSubmitted = true;
    if (this.verificationForm.invalid) {
      return;
    }

    this.verificationLoading = true;
    this.email = this.verificationForm.value;
    this.dataService.verifyEmail(this.email)
      .pipe(first())
      .subscribe(
        data => {
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
    // stop here if form is invalid
    if (this.registrationForm.invalid) {
      return;
    }
    this.registrationLoading = true;
    this.userToRegister = this.registrationForm.value;
    this.dataService.register(this.userToRegister)
      .pipe(first())
      .subscribe(
        data => {
          //this.alertService.success('Registration successful', true);
          // if (data.body == true) {
             alert("Registration completed. please login with your password and user name.")
             this.router.navigate(['/login']);
          // }
          // else {
          //   alert("Registration failed. A user with requested Email Address already Exists.")
          //   this.registrationLoading = false;
          // }

        },
        error => {
          // this.alertService.error(error);
          alert(error)
          this.registrationLoading = false;
        });
  }

  showVerificationForm() {
    this.isVisibleRegistrationForm = false;
    this.isVisibleVertificationForm = true;

  }

  showRegistrationForm() {
    this.isVisibleRegistrationForm = true;
    this.isVisibleVertificationForm = false;
  }

}




