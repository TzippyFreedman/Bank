import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(false); // {1}

  constructor(
    private router: Router
  ) {}


  login(userId: string){
   
    this.loggedIn.next(true);
    this.router.navigate(['/']);
  }
  get isLoggedIn() {
    return this.loggedIn.asObservable(); // {2}
  }
  logout(){
    this.loggedIn.next(false);
    this.router.navigate(['/login']);
  }
}
