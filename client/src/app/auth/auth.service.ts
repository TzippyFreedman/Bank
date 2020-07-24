import { Injectable } from '@angular/core';
import { BehaviorSubject, throwError } from 'rxjs';
import { Login } from '../login/login.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { HttpRequestHandlerService } from '../shared/services/http-request-handler.service';

const LOGIN_URL = 'user/login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggedIn = new BehaviorSubject<boolean>((this.userAccountAvailable()));

  constructor(private http: HttpClient, private requestHandlerService: HttpRequestHandlerService) { }

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

  login(loginObj: Login){
    return this.http.get<string>(this.requestHandlerService.createCompleteRoute(LOGIN_URL, environment.baseURL),
      { params: { email: loginObj.email, password: loginObj.password } })
      .pipe(catchError(this.requestHandlerService.handleError));
  }



  
}
