import { Injectable } from '@angular/core';
import { Product } from '../models/product';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginatedResult, QueryParams } from '../models/pagination';
import { catchError, map, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getPaginatedResult<T>(url: string, params: HttpParams, http: HttpClient) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();

    return http.get<T>(url, { observe: 'response', params }).pipe(
      map((response) => {
        const { body, headers } = response;

        if (body) {
          paginatedResult.result = body;
        }
        const pagination = headers.get('Pagination');
        if (pagination) {
          paginatedResult.pagination = JSON.parse(pagination);
        }
        return paginatedResult;
      }),
      catchError((error) => {
        console.error('Error occurred while fetching paginated result:', error);
        return throwError(error);
      })
    );
  }

  getProductsAllInOne(queryParams: QueryParams) {
    var params = this.setParams(queryParams);
    var url = this.baseUrl + 'products';

    return this.getPaginatedResult<Product[]>(url, params, this.http).pipe(
      map((response) => {
        return response;
      })
    );
  }
  setParams(queryParams: QueryParams): HttpParams {
    const shouldAddParam = (value: any, conditionValue: any) =>
      value !== conditionValue && value !== undefined;

    const paramConditions = {
      name: ['', queryParams.name],
      minprice: [0, queryParams.minprice],
      maxprice: [0, queryParams.maxprice],
      subCategoryName: ['', queryParams.subCategoryName],
      pageNumber: [1, queryParams.pageNumber],
    };

    let params = new HttpParams();

    for (const [key, [condition, value]] of Object.entries(paramConditions)) {
      if (shouldAddParam(value, condition)) {
        params = params.set(key, value!.toString());
      }
    }

    console.log(params);
    return params;
  }
}
