import { CommuncationService } from './../../../services/internal/communcation.service';
import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { PaginationResult } from 'src/app/models/PaginationResult';
import { OrderUpdate } from 'src/app/models/order';
import { AuthService } from 'src/app/services/auth/auth.service';
import { OrdersService } from 'src/app/services/orders/orders.service';
import { StocksService } from 'src/app/services/stocks/stocks.service';

@Component({
  selector: 'update-order',
  templateUrl: './update-order.component.html',
  styleUrls: ['./update-order.component.css'],
})
export class UpdateOrderComponent implements OnInit {
  orderFormupdate!: FormGroup;
  stocks: any = [];
  @Input() order: any;
  constructor(
    private fb: FormBuilder,
    public modalRef: BsModalRef,
    private ordersService: OrdersService,
    private stocksService: StocksService,
    private authService: AuthService,
    private toastrService: ToastrService
  ) {}
  async ngOnInit(): Promise<void> {
   await this.getAllStocks();
    this.createForm();
    console.log(this.order); // Accessing flagForUpdate
  }

  
  getAllStocks() {
    this.stocksService
      .getWithPagination(-1)
      .subscribe((response: PaginationResult) => {
        this.stocks = response.items;
        console.log(this.stocks);
      });
  }
  createForm() {
    this.orderFormupdate = this.fb.group({
      stockId: [this.order.stockId, Validators.required],
      order_id:[this.order.id],
      quantity: [this.order.quantity, Validators.required],
    });
  }
  get orderFormupdateControls() {
    return this.orderFormupdate.controls;
  }

  onSave() {
    let data = {
      userId: this.authService.getUserId(),
      ...this.orderFormupdate.value,
    };

    debugger;
    this.ordersService.update(data).subscribe((response: any) => {
      this.toastrService.show('Order updated Succesfully');
      this.ordersService.orderSource.getValue();
    });
  }
}
