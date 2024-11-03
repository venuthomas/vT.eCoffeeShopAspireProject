import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { OrderModel } from '../../shared/models/order.model';
// import {env} from "process";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private hubConnection: signalR.HubConnection;
  private orderReceivedSubject = new Subject<OrderModel>();
  public orderReceived$ = this.orderReceivedSubject.asObservable();
  private apiUrlAdmin = 'http://localhost:7004/api/Hub';// 'http://localhost:7005/aspire-admin.api/api/Hub'; //process.env['services__adminservice__http__0'] || 'https://localhost:8005/api/Hub';
  constructor() {
    console.log('adminservie', this.apiUrlAdmin)
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.apiUrlAdmin}/orderHub`) // Backend URL
      .build();

    this.hubConnection.start()
      .then(() => console.log('Connection to OrderHub started'))
      .catch(err => console.error('Error while starting connection: ', err));

    this.hubConnection.on('NewOrderRecived', (message: OrderModel) => {
      this.orderReceivedSubject.next(message); // Notify components about the new order
    });
  }

}
