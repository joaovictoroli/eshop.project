import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddCategory, AddSubCategory, Category } from '../models/categories';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  baseUrl = environment.apiUrl;

  private updateSubject = new Subject<void>();
  updateObservable$ = this.updateSubject.asObservable();

  constructor(private http: HttpClient) {}

  getCategoriesNSubCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'categories');
  }

  addCategory(addCategory: AddCategory) {
    return this.http.post(
      this.baseUrl + 'categories/add-category',
      addCategory,
      {
        observe: 'response',
      }
    );
  }

  addSubCategory(addSubCategory: AddSubCategory) {
    return this.http.post(
      this.baseUrl + 'categories/add-subcategory',
      addSubCategory,
      {
        observe: 'response',
      }
    );
  }

  deleteCategory(categoryId: number) {
    return this.http.delete(
      this.baseUrl + 'categories/delete-category/' + categoryId,
      {
        observe: 'response',
      }
    );
  }

  deleteSubCategory(subCategoryId: number) {
    return this.http.delete(
      this.baseUrl + 'categories/delete-subcategory/' + subCategoryId,
      {
        observe: 'response',
      }
    );
  }

  notifyUpdate() {
    console.log('Emitindo atualização...'); // Adicione isso

    this.updateSubject.next();
  }
}
