import { map, mapTo, mergeMap } from 'rxjs';
import { SignalRService } from './../../services/signalR/signal-r.service';
import { StocksService } from './../../services/stocks/stocks.service';
import { Component } from '@angular/core';
import { animate, style, transition, trigger } from '@angular/animations';
import { Stock } from 'src/app/models/stock';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Pagination, PaginationResult } from 'src/app/models/PaginationResult';

@Component({
  selector: 'stocks',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.css'],
  animations: [
    trigger('fadeInOut', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('1s', style({ opacity: 1 })),
      ]),
    ]),
  ],
})
export class StocksComponent {
  maxSize = 4;
  pageSize: number = 7;
  pagination!: Pagination;
  stocks: Stock[] = [];
  constructor(
    private stocksService: StocksService,
    private signalRService: SignalRService
  ) {
    this.signalRService.stockList$.subscribe((updatedStocks: any) => {
      this.stocks = this.stocks.map((stock: any, index: any) => ({
        ...stock,
        price: updatedStocks[index]?.price || stock.price,
      }));
    });
  }

  ngOnInit() {
    this.getAll(1);
  }

  getAll(pageNumber: number) {
    let data = {
      pageNumber: pageNumber,
      pageSize: this.pageSize,
    };
    this.stocksService
      .getWithPagination(pageNumber, this.pageSize)
      .subscribe((response: PaginationResult) => {
        this.stocks = response.items;
        this.pagination = response.pagination;
        console.log(response);
      });
  }
  pageChanged(event: PageChangedEvent) {
    this.pagination.currentPage = event.page;
    console.log(this.pagination.currentPage);
    this.getAll(this.pagination.currentPage);
  }
}
