import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggedIn = new BehaviorSubject<boolean>(false); // {1}

  get isLoggedIn() {
    return this.loggedIn.asObservable(); // {2}
  }
  constructor(private router: Router) { }

  login(userId: string){
   // if (user.userName !== '' && user.password !== '' ) { // {3}
    this.loggedIn.next(true);
   // this.router.navigate(['/']);
  }

  logout(){
    this.loggedIn.next(false);
    this.router.navigate(['/login']);
  }
}
