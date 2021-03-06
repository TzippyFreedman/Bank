import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../shared/services/data.service';
import { CookieService } from 'ngx-cookie-service';
import { UserAccount } from './user-account.model';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {

  account: UserAccount;
  accountId: string;

  constructor(private route: ActivatedRoute, public dataService: DataService, private cookieService: CookieService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(paramMap => {
      this.accountId = paramMap.get('userFileId');
    });

    this.dataService.getAccountDetails(this.accountId)
      .subscribe(
        Response => {
          this.account = Response;
        },
        error => {
          alert(error);
        });
  }
}
