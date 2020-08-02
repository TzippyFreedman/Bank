import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { Register } from '../../register/register.model';
import { UserAccount } from 'src/app/user-account/user-account.model';
import { HttpRequestHandlerService } from './http-request-handler.service';
import { Transfer } from 'src/app/transfer/transfer.model';
import { HistoryRequestParams } from 'src/app/operations-history/history-request-params.model';
import { HistoryResponse } from 'src/app/operations-history/history-response.model';
import { AuthService } from 'src/app/auth/auth.service';

const ACCOUNT_URL = 'user/getAccountDetails/'
const TRANSACTION_URL = 'transaction';
const OPERATIONS_HISTORY_URL = 'operationsHistory';

@Injectable({
  providedIn: 'root',
})
export class DataService {

  constructor(private http: HttpClient, private requestHandlerService: HttpRequestHandlerService, private authService: AuthService) { }

  public getAccountDetails = (userId: String) => {
    return this.http.get<UserAccount>(this.requestHandlerService.createCompleteRoute(ACCOUNT_URL + userId, environment.userServiceBaseURL))
      .pipe(catchError(this.requestHandlerService.handleError));
  }

  public commitTransaction = (transfer: Transfer) => {
    return this.http.post<void>(this.requestHandlerService.createCompleteRoute(TRANSACTION_URL, environment.transactionServiceBaseURL), transfer)
      .pipe(catchError(this.requestHandlerService.handleError));
  }

  public getTransaction = (transferId: string) => {
    debugger;
    return this.http.get<Transfer>(this.requestHandlerService.createCompleteRoute(`${TRANSACTION_URL}/${transferId}`, environment.transactionServiceBaseURL))
      .pipe(catchError(this.requestHandlerService.handleError));
  }

  public getOperationsHistory = (operationRequestParams: HistoryRequestParams) => {
    let params = new HttpParams();
    params = params.append('accountId', operationRequestParams.accountId.toString());
    params = params.append('pageIndex', operationRequestParams.pageIndex.toString());
    params = params.append('pageSize', operationRequestParams.pageSize.toString());
    params = params.append('sortField', operationRequestParams.sortField.toString());
    params = params.append('sortDirection', operationRequestParams.sortDirection.toString());
    params = params.append('searchString', operationRequestParams.filter.toString());
    params = params.append('isFilterChanged', operationRequestParams.isFilterChanged.toString());
    return this.http.get<HistoryResponse>(this.requestHandlerService.createCompleteRoute(OPERATIONS_HISTORY_URL, environment.userServiceBaseURL), { params: params })
      .pipe(catchError(this.requestHandlerService.handleError));
  }

}