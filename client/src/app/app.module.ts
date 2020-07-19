import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RouterModule } from '@angular/router';
import { UserAccountComponent } from './user-account/user-account.component';
import {HttpClientModule} from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    UserAccountComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    // CommonModule,
    ReactiveFormsModule,

    HttpClientModule,
    RouterModule.forRoot([
      {path:'register',component:RegisterComponent},
     {path:'login',component:LoginComponent},
     {path:'user/:userFileId',component:UserAccountComponent},
     {path:"",redirectTo:'/login',pathMatch:"full"}
]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
