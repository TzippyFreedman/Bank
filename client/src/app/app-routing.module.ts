import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { UserAccountComponent } from './user-account/user-account.component';
import { AuthGuard } from './auth/auth.guard';
import { TransferComponent } from './transfer/transfer.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { TransferDetailsComponent } from './transfer-details/transfer-details.component';
import { AdminHomePageComponent } from './admin-home-page/admin-home-page.component';


const routes: Routes = [
  { path: 'register', component: RegisterComponent },
  { path: 'transfer-details/:transferId', component: TransferDetailsComponent, canActivate: [AuthGuard] },
  { path: 'transfer', component: TransferComponent, canActivate: [AuthGuard] },
  { path: 'history', component: OperationsHistoryComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'user/:userFileId', component: UserAccountComponent, canActivate: [AuthGuard] },
  { path: 'admin-homePage/:userFileId', component: AdminHomePageComponent, canActivate: [AuthGuard] },
  { path: "", redirectTo: '/login', pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
