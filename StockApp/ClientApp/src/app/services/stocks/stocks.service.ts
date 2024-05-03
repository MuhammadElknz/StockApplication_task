import { Data } from '@angular/router';
import { DataService } from './../http/data.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StocksService {
  constructor(private dataService: DataService) {}

  getAll() {
    return this.dataService.get('/stock/getall', 'application/json');
  }

  getWithPagination(pageNumber: number, pageSize: number = 10, param?: any) {
    return this.dataService.getWithPagination(
      '/stock/getall',
      'application/json',
      '',
      pageNumber,
      pageSize
    );
  }
}
