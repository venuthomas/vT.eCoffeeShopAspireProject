import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerModel } from '../../shared/models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private apiUrlCustomer = '/aspire.api/api/Customer';
  constructor(private http: HttpClient) { }
saveCustomer(customer: CustomerModel ): any {
  return this.http.post(`${this.apiUrlCustomer}/save`, customer);
}
}
