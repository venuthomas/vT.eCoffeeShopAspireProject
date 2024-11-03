import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { OrderModel } from '../../../shared/models/order.model';
import { OrderStatusEnum } from '../../../shared/models/orderStatus.enum';

@Component({
  selector: 'app-order-popup',
  templateUrl: './order-popup.component.html',
  styleUrls: ['./order-popup.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, MatTableModule, MatDialogModule]
})
export class OrderPopupComponent implements OnInit {

   order!: OrderModel;
  status = OrderStatusEnum;
  constructor(
    public dialogRef: MatDialogRef<OrderPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { this.order = data }

  ngOnInit() {
  }

  displayedColumns: string[] = ['name', 'price'];

  updateStatus(status: OrderStatusEnum) {
    this.order.orderStatus = status;  
    this.dialogRef.close({ ordersId: this.order.ordersId, status: status });
  }


  closeDialog(): void {
    this.dialogRef.close();
  }
}
