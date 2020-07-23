import { Injectable } from '@angular/core';
import { BehaviorSubject, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { Login } from '../login/login.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

const LOGIN_URL = 'user/login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggedIn = new BehaviorSubject<boolean>((this.userAccountAvailable()));

  constructor(private http: HttpClient) { }

  private userAccountAvailable(): boolean {
    return !!sessionStorage.getItem('userAccountId');
  }

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  setLoggedIn(userAccountId: string) {
    this.loggedIn.next(true);
    sessionStorage.setItem('userAccountId', userAccountId)
  }

  logout() {
    this.loggedIn.next(false);
    sessionStorage.removeItem('userAccountId');
  }

  public login = (loginObj: Login) => {
    return this.http.get<string>(this.createCompleteRoute(LOGIN_URL, environment.baseURL),
      { params: { email: loginObj.email, password: loginObj.password } })
      .pipe(catchError(this.handleError));
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

  private handleError(error: HttpErrorResponse) {
    // A client-side or network error occurred. Handle it accordingly.
    if (error.error.errorMessage) {
      return throwError(new Error(`An error occurred:${error.error.errorMessage}`));
    }
    else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    return throwError(
      'An Error occured ; please try again later.');
  };
}
