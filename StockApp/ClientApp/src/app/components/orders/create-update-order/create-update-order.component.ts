import { CommuncationService } from './../../../services/internal/communcation.service';
import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { PaginationResult } from 'src/app/models/PaginationResult';
import { AuthService } from 'src/app/services/auth/auth.service';
import { OrdersService } from 'src/app/services/orders/orders.service';
import { StocksService } from 'src/app/services/stocks/stocks.service';

@Component({
  selector: 'create-update-order',
  templateUrl: './create-update-order.component.html',
  styleUrls: ['./create-update-order.component.css'],
})
export class CreateUpdateOrderComponent implements OnInit {
  orderForm!: FormGroup;
  stocks: any = [];
  constructor(
    private fb: FormBuilder,
    public modalRef: BsModalRef,
    private ordersService: OrdersService,
    private stocksService: StocksService,
    private authService: AuthService,
    private toastrService: ToastrService
  ) {}
  ngOnInit(): void {
    this.createForm();
    this.getAllStocks();
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
    this.orderForm = this.fb.group({
      stockId: ['', Validators.required],
      quantity: ['', Validators.required],
    });
  }
  get orderFormControls() {
    return this.orderForm.controls;
  }

  onSave() {
    let data = {
      userId: this.authService.getUserId(),
      ...this.orderForm.value,
    };

    debugger
    this.ordersService.create(data).subscribe((response: any) => {
      this.toastrService.show('Order Added Succesfully');
      this.ordersService.orderSource.next(response);
    });
  }
}
