import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {RouterOutlet} from "@angular/router";
import {CoffeeOrderComponent} from "./order/comp/coffee-order/coffee-order.component";
import {CustomerEntryComponent} from "./order/comp/customer-entry/customer-entry.component";
import {OrderDashboardComponent} from "./admin-panel/comp/orderDashboard/orderDashboard.component";
import {HttpClientModule} from "@angular/common/http";
import {CommonModule} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatTableModule} from "@angular/material/table";
import {ToastrModule} from "ngx-toastr";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    RouterOutlet,
    CoffeeOrderComponent,
    CustomerEntryComponent,
    OrderDashboardComponent,
    HttpClientModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    ToastrModule.forRoot(),
    AppRoutingModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
