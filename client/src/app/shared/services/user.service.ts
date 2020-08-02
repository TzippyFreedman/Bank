import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpRequestHandlerService } from './http-request-handler.service';
import { AuthService } from 'src/app/auth/auth.service';
import { Register } from 'src/app/register/register.model';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

const REGISTER_URL = 'user';
const VERIFICATION_URL = 'verificationCode/verifyEmail';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private requestHandlerService: HttpRequestHandlerService, private authService: AuthService) { }

  public register = (user: Register) => {
    return this.http.post<void>(this.requestHandlerService.createCompleteRoute(REGISTER_URL, environment.userServiceBaseURL), user)
      .pipe(catchError(this.requestHandlerService.handleError));
  }

  public verifyEmail = (email: string) => {
    return this.http.post<void>(this.requestHandlerService.createCompleteRoute(VERIFICATION_URL, environment.userServiceBaseURL), email)
      .pipe(catchError(this.requestHandlerService.handleError));
  }
}
