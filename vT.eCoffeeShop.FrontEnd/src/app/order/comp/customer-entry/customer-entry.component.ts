import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterOutlet } from '@angular/router';
import { CustomerService } from '../../services/customer.service';
import { CustomerModel } from '../../../shared/models/customer.model';

@Component({
  selector: 'app-customer-entry',
  standalone: true,
  imports: [CommonModule, RouterOutlet, FormsModule],
  templateUrl: './customer-entry.component.html',
  styleUrls: ['./customer-entry.component.css'],
  providers: [CustomerService]
})
export class CustomerEntryComponent {
  customerName: string = '';

  constructor(private customerService: CustomerService,
    private router: Router
  ) { }
  submitName() {
    if (this.customerName) {
      console.log('Customer Name:', this.customerName);
      let customer: CustomerModel ={
        customerName: this.customerName
      }
       this.customerService.saveCustomer(customer).subscribe((result: any) =>  {
        customer.customerId=result.id
        this.router.navigate(['/order'], { queryParams: { customerId: result.id, customerName: this.customerName } });
       });
      // You can add logic here to handle the submitted name, e.g., save it or use it in another component
    }
  }
}
