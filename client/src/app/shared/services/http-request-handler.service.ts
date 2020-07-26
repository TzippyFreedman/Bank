import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpRequestHandlerService {

  constructor() { }
  
   createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

   generateHeaders = () => {
    return {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Access-Control-Expose-Headers': '*', 'Access-Control-Allow-Credentials': 'true', 'withCredentials': 'true','Access-Control-Allow-Origin':'*' }),
      observe: 'response' as 'response'
    }
  }

   handleError(error: HttpErrorResponse) {
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
