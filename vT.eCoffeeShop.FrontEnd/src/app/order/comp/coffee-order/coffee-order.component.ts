import { Component, OnInit } from '@angular/core';
import { CoffeeItemModel } from '../../../shared/models/coffeeItem.model';
import { CoffeeService } from '../../services/coffee.service';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { HttpClient, HttpClientModule, HttpHandler } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { OrderModel } from '../../../shared/models/order.model';
import { OrderStatusEnum } from '../../../shared/models/orderStatus.enum';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-coffee-order',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './coffee-order.component.html',
  styleUrls: ['./coffee-order.component.css'],
  providers:  [ CoffeeService ]
})
export class CoffeeOrderComponent implements OnInit {

  coffeeItems: CoffeeItemModel[] = [];  
   order : OrderModel = {
     customerId: '',
     orderItems: [],
     totalAmount: 0,
     customerName: '',
     totalQty: 0
   };
  totalAmount = 0;
  totalItems = 0;
  

  constructor(private coffeeService: CoffeeService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.order.customerId = params['customerId'];
      this.order.customerName = params['customerName'];
      if (!this.order.customerId) {
        // Redirect to customer entry page if name is null or empty
        this.router.navigate(['/customer-entry']);
      }
      else 
      this.loadCoffeeItems();
    });
  }

  loadCoffeeItems(): void {
    console.log('Loadingggg...')
    this.coffeeService.getCoffeeItems().subscribe(
      (data: CoffeeItemModel[]) => this.coffeeItems = data,
      error => console.error('Error fetching coffee items222', error)
    );
  }

  // orderCoffee(coffeeItem: CoffeeItemModel): void {
  // this.order.orderItems.push(coffeeItem)
    
  // }

  placeOrder(){
    this.order.orderStatus = OrderStatusEnum.Accepted
    this.order.totalAmount = this.totalAmount
    this.coffeeService.orderCoffee(this.order).subscribe(
      response =>  this.toastr.success(`Ordered ID: ${response.id}`, response.message),
      error => console.error('Error ordering coffee', error)
    );
  }
  redirectToCustomerPage(){
    this.router.navigate(['/customer-entry']);
  }
  orderCoffee(item: any) {
    // Add item to basket
    const basketItem =  this.order.orderItems.find(b => b.name === item.name);
    if (basketItem) {
      basketItem.quantity++;
    } else {
       this.order.orderItems.push({ ...item, quantity: 1 });
    }
    this.calculateTotals();
  }

  addOrder() {
    // Logic to add a new order
    console.log('Add Order button clicked');
  }



  increaseQuantity(item: any) {
    const basketItem =  this.order.orderItems.find(b => b.name === item.name);
    if (basketItem) {
      basketItem.quantity++;
    } else {
       this.order.orderItems.push({ ...item, quantity: 1 });
    }
    this.calculateTotals();
  }

  decreaseQuantity(item: any) {
    const basketItem =  this.order.orderItems.find(b => b.name === item.name);
    if (basketItem && basketItem.quantity > 0) {
      basketItem.quantity--;
      if (basketItem.quantity === 0) {
         this.order.orderItems =  this.order.orderItems.filter(b => b.name !== item.name);
      }
    }
    this.calculateTotals();
  }

  calculateTotals() {
    this.totalAmount =  this.order.orderItems.reduce((sum, item) => sum + (item.price * item.quantity), 0);
    this.totalItems =  this.order.orderItems.reduce((sum, item) => sum + item.quantity, 0);
  }

  getItemQuantity(item: any) {
    const basketItem =  this.order.orderItems.find(b => b.name === item.name);
    return basketItem ? basketItem.quantity : 0;
  }
}