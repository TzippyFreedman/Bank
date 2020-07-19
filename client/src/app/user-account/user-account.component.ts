import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IUser } from '../shared/models/IUser';
import { DataService } from '../shared/services/data.service';
import { cwd } from 'process';
import { IAccount } from '../shared/models/IAccount';
import { CookieService } from 'ngx-cookie-service';
@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {
  account: IAccount;
  accountId: string;
  constructor(private route:ActivatedRoute, private http: DataService,private cookieService: CookieService) { }

  
  ngOnInit(): void {
    this.route.paramMap.subscribe( paramMap => {
      this.accountId = paramMap.get('userFileId');
      this.cookieService.set( 'userAccountId',  this.accountId   );
      // this.cookieValue = this.cookieService.get('Test');
  });

  this.http.getAccountDetails(this.accountId)
  .subscribe(
    result => {
this.account = result;
debugger;  },
    error => {
      debugger;
        alert(error);

    });
  }


  this.http.getAccountDetails(this.accountId)
  .subscribe(
    result => {
this.account = result;
debugger;  },
    error => {
      debugger;
        alert(error);

    });
  }
}
