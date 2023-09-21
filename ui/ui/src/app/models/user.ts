export interface User {
  username: string;
  token: string;
  knownAs: string;
  roles: string[];
  addresses: UserAddress[];
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

export class AddressToRegister {
  CEP: string = '';
  numero: number = 0;
  apartamento: number = 0;
  infoAdicional: string = '';

  // constructor(
  //   CEP: string,
  //   numero: number,
  //   apartamento: number,
  //   infoAdicional: string
  // ) {
  //   this.CEP = CEP;
  //   this.numero = numero;
  //   this.apartamento = apartamento;
  //   this.infoAdicional = infoAdicional;
  // }

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
