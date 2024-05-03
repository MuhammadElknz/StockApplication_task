import { Injectable } from '@angular/core';
import { DataService } from '../http/data.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private dataService: DataService) {}

  login(data: any) {
    return this.dataService.create('/account/Login', data, 'application/json');
  }
}
