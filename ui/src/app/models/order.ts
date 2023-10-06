export interface Order {
  orderProducts: OrderProduct[];
}

interface OrderProduct {
  productName: string;
  quantity: number;
}
