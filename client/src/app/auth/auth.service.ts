import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggedIn = new BehaviorSubject<boolean>((this.userAccountAvailable()));

  constructor() { }

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
}
