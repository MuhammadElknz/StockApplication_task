import { Stock } from './stock';

export interface Order {
  id: number;
  stock: Stock;
  price: number;
  quantity: number;
  createdBy: string;
  // Add other properties as needed
}

export interface OrderUpdate {
  flagForUpdate: boolean;
  order: any; // Add any additional properties here
}