import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Login } from '../login/login.model';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { HttpRequestHandlerService } from '../shared/services/http-request-handler.service';

const LOGIN_URL = 'user/login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggedIn = new BehaviorSubject<boolean>((this.isUserAccountAvailable()));

  constructor(private http: HttpClient, private requestHandlerService: HttpRequestHandlerService) { }

  private isUserAccountAvailable(): boolean {
    return !!sessionStorage.getItem('userAccountId');
  }
   getUserAccountId(): string {
    return window.sessionStorage.getItem('userAccountId');
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
    return this.http.get<string>(this.requestHandlerService.createCompleteRoute(LOGIN_URL, environment.userServiceBaseURL),
      { params: { email: loginObj.email, password: loginObj.password } })
      .pipe(catchError(this.requestHandlerService.handleError));
  }
}
