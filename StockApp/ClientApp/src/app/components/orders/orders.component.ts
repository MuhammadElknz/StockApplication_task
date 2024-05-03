import { OrdersService } from 'src/app/services/orders/orders.service';
import { BehaviorSubject } from 'rxjs';
import { SignalRService } from './../../services/signalR/signal-r.service';
import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CreateUpdateOrderComponent } from './create-update-order/create-update-order.component';
import { Order, OrderUpdate } from 'src/app/models/order';
import { UpdateOrderComponent } from './update-order/update-order.component';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit, OnDestroy {
  modalRef?: BsModalRef;
  orders: Order[] = [];
  forupdate = true;
  constructor(
    private signalRService: SignalRService,
    private modalService: BsModalService,
    private ordersService: OrdersService,
    private toastrService: ToastrService,
    private authService: AuthService
  ) {
    this.signalRService.startConnection();

    this.signalRService.addUpdatedStocksListener((response) => {});

    this.getAddOrUpdatedOrder();
  }

  ngOnInit(): void {
    this.getAllOrders();
  }

  getAllOrders() {
    this.ordersService.getAll().subscribe((response: any) => {
      this.orders = response;
    });
  }
  openModal() {
    this.modalRef = this.modalService.show(CreateUpdateOrderComponent);
  }

  openEditModal(order:any) {
    const initialState = {
      order: order
    };
    this.modalRef = this.modalService.show(UpdateOrderComponent, { initialState });
  
  }
  getAddOrUpdatedOrder() {
    this.ordersService.orderThread$.subscribe((response: Order) => {
      this.orders.push(response);
      this.modalRef?.hide();
    });
  }
  ngOnDestroy(): void {
    this.signalRService.stopHubConnection();
  }

  deleteorder(id:any){
    let data = {
      UserId: this.authService.getUserId(),
      Order_id:id
    };
    debugger
    this.ordersService.delete(data).subscribe((response: any) => {
      this.toastrService.show('Order deleted Succesfully');
      this.ordersService.orderSource.getValue();
    });
  }
}
