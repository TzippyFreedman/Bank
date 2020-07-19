import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {  FormGroup, Validators, FormControl } from '@angular/forms';
import { ILogin } from '../shared/models/ILogin';
import { DataService } from '../shared/services/data.service';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
     
    loginForm: FormGroup;
    loading = false;
    submitted = false;
    userToAuthenticate = <ILogin>{};

    constructor(private router:Router,private http: DataService)  { }
  
    
  ngOnInit(): void {
    this.loginForm = new FormGroup({
      'username': new FormControl('',[
      Validators.required,
      Validators.minLength(2),
      Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")


    ]),
    'password': new FormControl('',[
      Validators.required,
      Validators.minLength(6)
    ])
  });


 
  }
  get formControls() { return this.loginForm.controls; }

  onSubmit(){
    
    this.submitted = true;
        // stop here if form is invalid
        if (this.loginForm.invalid) {
            return;
        }
        this.loading = true;
        this.userToAuthenticate.password=this.formControls.password.value;
        this.userToAuthenticate.email= this.formControls.username.value;
        this.http.login(this.userToAuthenticate)
            .subscribe(
                result => {
                    this.router.navigate(['user', result ]);
                },
                error => {
                    alert(error);
                    this.loading = false;
                });
    }
}


