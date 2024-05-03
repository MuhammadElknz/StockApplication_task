import { Injectable } from '@angular/core';
import { DataService } from '../http/data.service';
import { BehaviorSubject } from 'rxjs';
import { Order } from 'src/app/models/order';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  public orderSource = new BehaviorSubject<any>(null);
  orderThread$ = this.orderSource.asObservable();

  constructor(private dataService: DataService, private auth: AuthService) {}

  getById(id: number) {
    return this.dataService.get(`/order/${id}`, 'application/json');
  }

  getAll() {
    return this.dataService.get('/order/GetAll', 'application/json');
  }

  create(data: any) {
    return this.dataService.create('/Order/Create', data, 'application/json');
  }
  update(data: any) {
    return this.dataService.create('/Order/Update', data, 'application/json');
  }
  delete(data: any) {
    return this.dataService.create('/Order/Delete', data, 'application/json');
  }
}
