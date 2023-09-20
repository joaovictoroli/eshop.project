import { Injectable } from '@angular/core';
import { Product } from '../models/product';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  baseUrl = environment.apiUrl;
  members: Product[] = [];
  memberCache = new Map();
  constructor(private http: HttpClient) {}

  getProductsAllInOne() {
    return this.http.get<Product[]>(this.baseUrl + 'products');
  }
}
