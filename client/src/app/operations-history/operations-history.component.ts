import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
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

@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.css']
})
export class OperationsHistoryComponent implements OnInit {


  public columnHeaders: string[] = [
    "transactionId",
    "operationTime",
    "balance",
    "isDebit",
    "transactionAmount"
  ]
  public dataSource: MatTableDataSource<HistoryOperation>;
  public operationTotal: number;
  public noData: HistoryOperation[] = [<HistoryOperation>{}];
  public loading: boolean;
  public error$: Observable<boolean>;
  public filterSubject = new Subject<string>();

  private filter: string = "";
  //dont forget to unsubscribe!!
  private subscription: Subscription = new Subscription();
  public defaultSort: Sort = { active: 'operationTime', direction: 'asc' };
  public pathRequestParams: HistoryRequestParams = new HistoryRequestParams();


  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;


  constructor(private dataService: DataService,private authService: AuthService) {
    this.dataSource = new MatTableDataSource(this.noData);
    this.dataSource.sort = this.sort;
  }


  public ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this.noData);
  }


  public loadOperations(): Observable<HistoryResponse> {
    debugger;
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
      debounceTime(150),
      distinctUntilChanged(),
      tap((value: string) => {
        this.paginator.pageIndex = 0;
        this.paginator.length = this.operationTotal; // we should reset page index       
        this.filter = value;
      })
    );

    let sort$ = this.sort.sortChange.pipe(tap(() => this.paginator.pageIndex = 0));

    this.subscription.add(merge(filter$, sort$, this.paginator.page).pipe(
      tap(() => this.loadOperations().subscribe(res => {

        this.initializeData(res);
      }))
    ).subscribe());

  }

  initializeData(historyResponse: HistoryResponse): void {
    this.operationTotal = historyResponse.operationCount;
    this.dataSource.data = historyResponse.operationList.length ? historyResponse.operationList : this.noData;
  }
  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public retry(): void {

    //this.loadPaths();

  }
}

