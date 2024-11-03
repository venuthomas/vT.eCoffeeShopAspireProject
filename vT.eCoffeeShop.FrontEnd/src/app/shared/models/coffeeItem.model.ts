export interface CoffeeItemModel {
    coffeeItemID: string;
    name: string;
    quantity: number;
    description: string;
    price: number;
    weight: number;
    isAvailable: boolean;
    imageUrl?: string;
  }