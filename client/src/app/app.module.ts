import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserAccountComponent } from './user-account/user-account.component';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from './header/header.component';
import { TransferComponent } from './transfer/transfer.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input'


import {  MatDialogModule } from "@angular/material/dialog";
import { TransferDetailsComponent } from './transfer-details/transfer-details.component';
import { DebitCreditPipe } from './operations-history/debit-credit.pipe';
import { AdminHomePageComponent } from './admin-home-page/admin-home-page.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    UserAccountComponent,
    HeaderComponent,
    TransferComponent,
    OperationsHistoryComponent,
    TransferDetailsComponent,
    DebitCreditPipe,
    AdminHomePageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatPaginatorModule,
    MatTableModule,
    MatInputModule,
    MatSortModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
