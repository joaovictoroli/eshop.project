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

export interface RegisterAddress {
  numero: number;
  apartamento: number;
  infoAdicional: string;
}
