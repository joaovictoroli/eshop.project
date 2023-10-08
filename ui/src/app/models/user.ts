export interface User {
  username: string;
  token: string;
  knownAs: string;
  roles: string[];
  addresses: UserAddress[];
  orders: UserOrder[];
}

export interface UserAddress {
  id: number;
  cep: string;
  uf: string;
  bairro: string;
  complemento: string;
  numero: string;
  apartamento: string;
  infoAdicional: string;
  isMain: boolean;
}

export interface UserOrder {
  orderId: number;
  userId: number;
  products: UserOrderProduct[];
  orderAddress: UserOrderAddress;
  totalPrice: number;
  submittedAt: Date;
}

export interface UserOrderProduct {
  quantity: number;
  price: number;
  productName: string;
  productImageUrl: string;
}

export interface UserOrderAddress {
  cep: string;
  uf: string;
  bairro: string;
  complemento: string;
  numero: number;
  apartamento: number;
  infoAdicinal: string;
}

export class AddressToRegister {
  CEP: string = '';
  numero: number = 0;
  apartamento: number = 0;
  infoAdicional: string = '';

  constructor(data: {
    CEP: string;
    numero: number;
    apartamento: number;
    infoAdicional: string;
  }) {
    this.CEP = data.CEP;
    this.numero = data.numero;
    this.apartamento = data.apartamento;
    this.infoAdicional = data.infoAdicional;
  }
}
