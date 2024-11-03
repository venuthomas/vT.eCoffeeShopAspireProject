import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { OrderModel } from '../../../shared/models/order.model';
import { OrderStatusEnum } from '../../../shared/models/orderStatus.enum';
import { MatTableModule } from '@angular/material/table';
import { OrderPopupComponent } from "../order-popup/order-popup.component";
import { MatDialog } from '@angular/material/dialog';
import { AdminPanelService } from '../../services/admin-panel.service';
import { NotificationService } from '../../services/Notification.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-orderDashboard',
  templateUrl: './orderDashboard.component.html',
  styleUrls: ['./orderDashboard.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, MatTableModule, OrderPopupComponent]
})
export class OrderDashboardComponent implements OnInit {
   orders: OrderModel[] = []
  //   {
  //     ordersId: "12345",
  //     customerName: 'John Doe',
  //     totalQty: 3,
  //     totalAmount: 15.00,
  //     orderItems: [
  //       {
  //         name: 'Espresso', price: 5.00,
  //         coffeeItemID: '',
  //         quantity: 0,
  //         description: '',
  //         weight: 0,
  //         isAvailable: false
  //       },
  //       {
  //         name: 'Cappuccino', price: 5.00,
  //         coffeeItemID: '',
  //         quantity: 0,
  //         description: '',
  //         weight: 0,
  //         isAvailable: false
  //       },
  //       {
  //         name: 'Latte', price: 5.00,
  //         coffeeItemID: '',
  //         quantity: 0,
  //         description: '',
  //         weight: 0,
  //         isAvailable: false
  //       }
  //     ],
  //     orderStatus: OrderStatusEnum.Accepted,
  //     customerId: 'asdasd'
  //   },
  //   // Add more orders as needed
  // ];

  status = OrderStatusEnum;
  newOrder?: OrderModel;
  constructor(public dialog: MatDialog, private adminService: AdminPanelService,
    private notificationService: NotificationService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.loadCoffeeItems();
    this.notificationService.orderReceived$.subscribe((message: OrderModel) => {
      this.newOrder = message;
      this.loadCoffeeItems();
      this.showNotification(message);
    });

  }

  showNotification(orderMessage: OrderModel) {
    // Logic to show notification and update order list with the new order
    this.toastr.success(`New Order Received Order ID: ${orderMessage.ordersId}`);
  }
  loadCoffeeItems(): void {
    this.adminService.getAllOrders().subscribe(
      (data: OrderModel[]) => this.orders = data,
      error => console.error('Error fetching coffee items222', error)
    );
  }

  displayedColumns: string[] = ['id', 'customerName', 'totalQty', 'totalAmount','currentstatus', 'status'];
  selectedOrder: OrderModel | null = null;

  showPopup(order: OrderModel) {
    const dialogRef = this.dialog.open(OrderPopupComponent, {
      maxWidth: '90vw',  // Adjust to your preference
      width: 'auto',      // Ensures it fits content without horizontal scrolling
      data: order
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Update the status of the order after the dialog is closed
        const orderIndex = this.orders.findIndex(o => o.ordersId === result.ordersId);
        if (orderIndex !== -1 && result.orderStatus != null) {
          this.orders[orderIndex].orderStatus = result.orderStatus ;        
          console.log('Updated orders array:', this.orders); // Log the updated orders array

        }
        console.log('Updated orders array111:', this.orders); // Log the updated orders array

      }
    });
  }

  closePopup() {
    this.selectedOrder = null;
  }
}
