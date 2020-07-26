import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { Register } from '../../register/register.model';
import { UserAccount } from 'src/app/user-account/user-account.model';
import { HttpRequestHandlerService } from './http-request-handler.service';
import { Transfer } from 'src/app/transfer/transfer.model';

const REGISTER_URL = 'user';
const ACCOUNT_URL = 'user/getAccountDetails/'
const VERIFICATION_URL = 'user/verifyEmail';
const TRANSFER_URL = 'transfer';
@Injectable({
  providedIn: 'root',
})
export class DataService {

  constructor(private http: HttpClient, private requestHandlerService: HttpRequestHandlerService) { }

  public register = (user: Register) => {
    return this.http.post<void>(this.requestHandlerService.createCompleteRoute(REGISTER_URL, environment.baseURL), user, this.requestHandlerService.generateHeaders())
      .pipe(catchError(this.requestHandlerService.handleError));

  }

  public verifyEmail = (email: string) => {
    return this.http.post<void>(this.requestHandlerService.createCompleteRoute(VERIFICATION_URL, environment.baseURL), email, this.requestHandlerService.generateHeaders())
      .pipe(catchError(this.requestHandlerService.handleError));

  }

  public getAccountDetails = (userId: String) => {
    return this.http.get<UserAccount>(this.requestHandlerService.createCompleteRoute(ACCOUNT_URL + userId, environment.baseURL))
      .pipe(catchError(this.requestHandlerService.handleError));

  }

  public transfer = (transfer: Transfer) => {
    debugger;
    return this.http.post<void>(this.requestHandlerService.createCompleteRoute(TRANSFER_URL, environment.baseURL), transfer, this.requestHandlerService.generateHeaders())
      .pipe(catchError(this.requestHandlerService.handleError));

  }

}