import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {OrderDashboardComponent} from "./admin-panel/comp/orderDashboard/orderDashboard.component";
import {CustomerEntryComponent} from "./order/comp/customer-entry/customer-entry.component";
import {CoffeeOrderComponent} from "./order/comp/coffee-order/coffee-order.component";

export const routes: Routes = [
  { path: 'order-dashboard', component: OrderDashboardComponent },
  { path: 'customer-entry', component: CustomerEntryComponent },
  { path: 'order', component: CoffeeOrderComponent },
  { path: '', redirectTo: '/customer-entry', pathMatch: 'full' },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
