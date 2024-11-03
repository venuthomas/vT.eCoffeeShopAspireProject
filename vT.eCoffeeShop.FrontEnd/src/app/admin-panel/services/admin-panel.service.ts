import { Injectable } from '@angular/core';
import { OrderModel } from '../../shared/models/order.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminPanelService {
  private apiUrlAdmin = '/aspire-admin.api/api/AdminPanel';
  constructor(private http: HttpClient) { }
getAllOrders(): Observable<OrderModel[]> {
   return this.http.get<OrderModel[]>(`${this.apiUrlAdmin}/getallorders`);
 }
}
