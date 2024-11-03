import { CoffeeItemModel } from "./coffeeItem.model";
import { OrderStatusEnum } from "./orderStatus.enum";

export interface OrderModel {
    ordersId?: string; // Guid in C# is string in TypeScript
    customerId: string;
    customerName: string;
    orderDate?: Date| undefined;
    orderStatus?: OrderStatusEnum | undefined;
    orderItems: CoffeeItemModel[];
    totalQty: number
    totalAmount: number;
    createdAt?: Date | undefined;
    updatedAt?: Date| undefined;
  }