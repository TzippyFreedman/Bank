import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../shared/services/data.service';
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

  constructor(private route: ActivatedRoute, private http: DataService, private cookieService: CookieService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(paramMap => {
      this.accountId = paramMap.get('userFileId');
      //this.cookieService.set( 'userAccountId',  this.accountId );
    });

    this.http.getAccountDetails(this.accountId)
      .subscribe(
        result => {
          this.account = result;
        },
        error => {
          alert(error);
        });
  }


}

// logOut:void()
// {

// }