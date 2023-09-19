import { Injectable } from '@angular/core';
import { Product } from '../models/product';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  baseUrl = environment.apiUrl;
  members: Product[] = [];
  memberCache = new Map();
  constructor() {}
}
// export class MembersService {
//   baseUrl = environment.apiUrl;
//   members: Member[] = [];
//   memberCache = new Map();
//   user: User | undefined;
//   userParams: UserParams | undefined;

//   constructor(
//     private http: HttpClient,
//     private accountService: AccountService
//   )
