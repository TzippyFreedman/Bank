import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { ILogin } from '../models/ILogin';
import { environment } from 'src/environments/environment';
import { IUser } from '../models/IUser';
import { catchError } from 'rxjs/operators';

const LOGIN_URL='user/login';
const REGISTER_URL='user/';
const ACCOUNT_URL='user/getAccountDetails/'
@Injectable({

    providedIn: 'root',
  })
  export class DataService {
    constructor(private http:HttpClient) { }
    private handleError(error: HttpErrorResponse) {
      if (error.error.message  ) {
        // A client-side or network error occurred. Handle it accordingly.
        return throwError(new Error(`An error occurred:${error.error.message}`) );
      } 
      // if( error.error.errorMessage){
      //   return throwError(new Error(`An error occurred:${error.error.errorMessage} `));

      // }
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
    public register = (user:IUser) => {
      return this.http.post<boolean>(this.createCompleteRoute(REGISTER_URL, environment.baseURL),user,this.generateHeaders())
        .pipe(catchError(this.handleError));
  
    }

    public getAccountDetails = (userId:String) => {
      debugger;
      return this.http.get<Account>(this.createCompleteRoute(ACCOUNT_URL+ userId, environment.baseURL))
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