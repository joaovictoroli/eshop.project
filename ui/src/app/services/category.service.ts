import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Category } from '../models/categories';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getCategoriesNSubCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'categories');
  }
}
