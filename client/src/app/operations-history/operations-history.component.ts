import { Component, OnInit, ViewChild, ElementRef, OnDestroy, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { HistoryOperation } from './history-operation.model';
import { Observable, Subject, Subscription, merge } from 'rxjs';
import { Sort, MatSort } from '@angular/material/sort';
import { DataService } from '../shared/services/data.service';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { HistoryRequestParams } from './history-request-params.model';
import { HistoryResponse } from './history-response.model';
import { AuthService } from '../auth/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { Transfer } from '../transfer/transfer.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.css']
})
export class OperationsHistoryComponent implements OnInit, OnDestroy, AfterViewInit {

  public columnHeaders: string[] = [
    "transactionId",
    "operationTime",
    "balance",
    "isCredit",
    "transactionAmount"
  ]
  public dataSource: MatTableDataSource<HistoryOperation>;
  public operationTotal: number;
  public currentRowTransfer: Transfer;
  public noData: HistoryOperation[] = [<HistoryOperation>{}];
  public loading: boolean;
  public error$: Observable<boolean>;
  public filterSubject = new Subject<string>();
  private filter: string = "";
  private subscription: Subscription = new Subscription();
  public defaultSort: Sort = { active: 'operationTime', direction: 'asc' };
  public pathRequestParams: HistoryRequestParams = new HistoryRequestParams();


  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;


  constructor(private dataService: DataService, private authService: AuthService, public dialog: MatDialog, private router: Router) {
    this.dataSource = new MatTableDataSource(this.noData);
    this.dataSource.sort = this.sort;
  }


  public ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this.noData);
  }


  public loadOperations(): Observable<HistoryResponse> {
    this.pathRequestParams = {
      accountId: this.authService.getUserAccountId(),
      filter: this.filter.toLocaleLowerCase(),
      isFilterChanged: this.pathRequestParams.filter?.toLocaleLowerCase() == this.filter.toLocaleLowerCase() ? false : true,
      pageIndex: this.paginator.pageIndex,
      pageSize: this.paginator.pageSize,
      sortDirection: this.sort.direction,
      sortField: this.sort.active
    }
    return this.dataService.getOperationsHistory(this.pathRequestParams);
  }

  public ngAfterViewInit(): void {

    this.loadOperations().subscribe(res => {
      this.initializeData(res);
    });

    let filter$ = this.filterSubject.pipe(
      debounceTime(500),
      distinctUntilChanged(),
      tap((value: string) => {
        this.paginator.pageIndex = 0;
        this.paginator.length = this.operationTotal; // we should reset page index       
        this.filter = value;
      })
    );

    let sort$ = this.sort.sortChange.pipe(
      tap(() => this.paginator.pageIndex = 0));

    this.subscription.add(merge(filter$, sort$, this.paginator.page)
      .pipe(tap(() => this.loadOperations()
          .subscribe(res => {
            this.initializeData(res);
          }))
      ).subscribe());
  }

  initializeData(historyResponse: HistoryResponse): void {
    this.operationTotal = historyResponse.operationsTotal;
    this.dataSource.data = historyResponse.operationsList.length ? historyResponse.operationsList : this.noData;
  }


  selectRow(row) {
    this.router.navigate(['transfer-details', row['transactionId']]);
  }
  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
    //unsubscribe children
  }
}
