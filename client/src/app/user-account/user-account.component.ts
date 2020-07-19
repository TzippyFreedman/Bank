import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IUser } from '../shared/models/IUser';
import { DataService } from '../shared/services/data.service';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {
  user:IUser;
  userFileId:string;
  constructor(private route:ActivatedRoute, private http: DataService) { }

  
  ngOnInit(): void {
    this.route.paramMap.subscribe( paramMap => {
      this.userFileId = paramMap.get('userFileId');

  });

  this.http
}
}
