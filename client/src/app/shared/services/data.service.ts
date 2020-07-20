import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { ILogin } from '../models/ILogin';
import { environment } from 'src/environments/environment';
import { IUser } from '../models/IUser';
import { catchError } from 'rxjs/operators';
import { IAccount } from '../models/IAccount';
import { IRegister } from '../models/IRegister';

const LOGIN_URL='user/login';
const REGISTER_URL='user/';
const ACCOUNT_URL='user/getAccountDetails/'
const VERIFICATION_URL= 'user/verifyEmail';
@Injectable({

    providedIn: 'root',
  })
  export class DataService {
    constructor(private http:HttpClient) { }
    private handleError(error: HttpErrorResponse) {
    // A client-side or network error occurred. Handle it accordingly.
      if (error.error.errorMessage  ) {
        return throwError(new Error(`An error occurred:${error.error.errorMessage}`) );
      } 
      else {
        // The backend returned an unsuccessful response code.
        // The response body may contain clues as to what went wrong,
        console.error(
          `Backend returned code ${error.status}, ` +
          `body was: ${error.error}`);
      }
      return throwError(
        'Something bad happened; please try again later.');
    };


    public login = (loginObj:ILogin) => {   
      return this.http.get<string>(this.createCompleteRoute(LOGIN_URL, environment.baseURL),
      {params: {email:loginObj.email, password: loginObj.password }})
        .pipe(catchError(this.handleError));
    }

    public register = (user:IRegister) => {
      return this.http.post<void>(this.createCompleteRoute(REGISTER_URL, environment.baseURL),user,this.generateHeaders())
        .pipe(catchError(this.handleError));
  
    }

    public verifyEmail = (email:string) => {
      return this.http.post<void>(this.createCompleteRoute(VERIFICATION_URL, environment.baseURL), email, this.generateHeaders())
        .pipe(catchError(this.handleError));
  
    }

    public getAccountDetails = (userId:String) => {
      debugger;
      return this.http.get<IAccount>(this.createCompleteRoute(ACCOUNT_URL+ userId, environment.baseURL))
        .pipe(catchError(this.handleError));
  
    }
    private createCompleteRoute = (route: string, envAddress: string) => {
      return `${envAddress}/${route}`;
    }
   
    private generateHeaders = () => {
      return {
        headers: new HttpHeaders({'Content-Type': 'application/json','Access-Control-Expose-Headers':'*','Access-Control-Allow-Credentials': 'true','withCredentials': 'true'}),
        observe: 'response' as 'response'
      }
  }
   
  }