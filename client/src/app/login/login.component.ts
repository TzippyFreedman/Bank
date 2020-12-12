import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Login } from './login.model';
import { DataService } from '../shared/services/data.service';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  userToAuthenticate = <Login>{};

  constructor(private router: Router, private http: DataService, private authService: AuthService) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      'username': new FormControl('', [
        Validators.required,
        Validators.minLength(2),
        Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")
      ]),
      'password': new FormControl('', [
        Validators.required,
        Validators.minLength(6)
      ])
    });

  }
  get formControls() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;
    this.userToAuthenticate.password = this.formControls.password.value;
    this.userToAuthenticate.email = this.formControls.username.value;
    this.authService.login(this.userToAuthenticate)
      .subscribe(
        response => {
          this.authService.setLoggedIn(response.userId);
          response.isAdmin ? this.router.navigate(['user', response.userId]) : this.router.navigate(['admin-homePage', response.userId]);
        },
        error => {
          alert(error);
          this.loading = false;
        });
  }
}


