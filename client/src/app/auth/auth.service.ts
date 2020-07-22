import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggedIn = new BehaviorSubject<boolean>((this.userAccountAvailable()));

  private userAccountAvailable(): boolean {
  return !!sessionStorage.getItem('userAccountId');
  } // {1}

  get isLoggedIn() {
    return this.loggedIn.asObservable(); // {2}
  }
  constructor() { }

  setLoggedIn(userAccountId: string){
   // if (user.userName !== '' && user.password !== '' ) { // {3}
    this.loggedIn.next(true);
    sessionStorage.setItem('userAccountId',userAccountId)  }

  logout(){
    this.loggedIn.next(false);
    sessionStorage.removeItem('userAccountId');

  }
}
