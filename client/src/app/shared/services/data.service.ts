import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Login } from '../../login/login.model';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { Register } from '../../register/register.model';
import { UserAccount } from 'src/app/user-account/user-account.model';

const REGISTER_URL = 'user/';
const ACCOUNT_URL = 'user/getAccountDetails/'
const VERIFICATION_URL = 'user/verifyEmail';
@Injectable({
  providedIn: 'root',
})
export class DataService {

  constructor(private http: HttpClient) { }

  private a:number = 1;
  public aa(){
    this.a = undefined;
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


  

  // public register = (user: Register) => {
  //   return this.http.post<void>(this.createCompleteRoute(REGISTER_URL, environment.baseURL), user, this.generateHeaders())
  //     .pipe(catchError(this.handleError));

  // }

  public register(user: Register) {
    return this.http.put<void>(this.createCompleteRoute(REGISTER_URL, environment.baseURL), user, this.generateHeaders());
  }
  // public login = (loginObj: Login) => {
  //   return this.http.get<string>(this.createCompleteRoute(LOGIN_URL, environment.baseURL),
  //     { params: { email: loginObj.email, password: loginObj.password } })
  //     .pipe(catchError(this.handleError));
  // }
  public verifyEmail = (email: string) => {
    return this.http.post<void>(this.createCompleteRoute(VERIFICATION_URL, environment.baseURL), email, this.generateHeaders())
      .pipe(catchError(this.handleError));

  }

  public getAccountDetails = (userId: String) => {
    return this.http.get<UserAccount>(this.createCompleteRoute(ACCOUNT_URL + userId, environment.baseURL))
      .pipe(catchError(this.handleError));

  }
  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

  private generateHeaders = () => {
    return {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Credentials': 'true', 'withCredentials': 'true' }),
      observe: 'response' as 'response'
    }
  }

}