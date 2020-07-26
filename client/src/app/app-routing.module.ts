import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { UserAccountComponent } from './user-account/user-account.component';
import { AuthGuard } from './auth/auth.guard';
import { TransferComponent } from './transfer/transfer.component';


const routes: Routes = [
  { path: 'register', component: RegisterComponent },
  { path: 'transfer', component: TransferComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'user/:userFileId', component: UserAccountComponent, canActivate: [AuthGuard] },
  { path: "", redirectTo: '/login', pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
