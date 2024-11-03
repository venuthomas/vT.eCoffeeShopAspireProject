import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CoffeeItemModel } from '../../shared/models/coffeeItem.model';
// import * as process from 'process';
// import { env } from 'process';
import { OrderModel } from '../../shared/models/order.model';
// inpm install vite @vitejs/plugin-angular --save-devmport { env } from 'process';

@Injectable({
  providedIn: 'root'
})
export class CoffeeService {
  // private appServer = process.env["services__orderservice__https__0"] ||
  // process.env["services__orderservice__http__0"] ;

 // private apiUrl = `${this.appServer}/api/Coffee`;
 private apiUrlCoffee = '/aspire.api/api/Coffee';
 private apiUrlOrder = '/aspire.api/api/Order';

  constructor(private http: HttpClient) { }

  getCoffeeItems(): Observable<CoffeeItemModel[]> {
   // console.log('ssadasdas', this.appServer)
    console.log('getCoffeeItems1111', `${this.apiUrlCoffee}/fetchallcoffee`)
  //  return inject(HttpClient).get<CoffeeItem[]>(`${this.apiUrl}/fetchallcoffee`);
    return this.http.get<CoffeeItemModel[]>(`${this.apiUrlCoffee}/fetchallcoffee`);
  }

  orderCoffee(coffeeItem: OrderModel): Observable<any> {
    return this.http.post(`${this.apiUrlOrder}/place-order`, coffeeItem);
  }
}
