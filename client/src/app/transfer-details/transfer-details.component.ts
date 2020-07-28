import { Component, OnInit } from '@angular/core';
import { Transfer } from '../transfer/transfer.model';
import { Router, ActivatedRoute } from '@angular/router';
import { DataService } from '../shared/services/data.service';

@Component({
  selector: 'app-transfer-details',
  templateUrl: './transfer-details.component.html',
  styleUrls: ['./transfer-details.component.css']
})
export class TransferDetailsComponent implements OnInit {

  constructor(private dataService: DataService,private route: ActivatedRoute) { }
  public transfer:Transfer;
  ngOnInit(): void {
    this.route.paramMap.subscribe(paramMap => {
    this.dataService.getTransfer(paramMap.get('transferId'))
     .subscribe(transfer=>{         
        this.transfer=transfer;
   },
    error=>{
 alert(error);
     });
    });
  }

}
