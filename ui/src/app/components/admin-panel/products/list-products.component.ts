import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Category, SubCategory } from 'src/app/models/categories';
import { Pagination, QueryParams } from 'src/app/models/pagination';
import { Product } from 'src/app/models/product';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-list-products',
  templateUrl: './list-products.component.html',
  styleUrls: ['./list-products.component.scss'],
})
export class ListProductsComponent {
  categories: Category[] | undefined;
  products: Product[] | undefined;
  queryParams: QueryParams = new QueryParams();
  pagination: Pagination | undefined;

  displayedColumns: string[] = [
    'id',
    'name',
    'description',
    'technicalInfo',
    'imageUrl',
    'price',
    'delete',
  ];

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService
  ) {}

  ngOnInit() {
    this.categoryService.getCategoriesNSubCategories().subscribe({
      next: (value) => {
        this.categories = value;
      },
      error: (error) => {
        console.error('Error fetching products:', error);
      },
      complete: () => {
        console.log('Fetching products completed.');
      },
    });

    this.onFilter();
  }

  onFilter(page?: number) {
    this.queryParams.pageNumber = page ?? 1;

    this.productService.getProductsList(this.queryParams).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          console.log(response.result);
          this.products = response.result;
          this.queryParams.pageNumber = response.pagination.currentPage;
          this.pagination = response.pagination;
        }
      },
      error: (error) => {
        console.error('Error fetching products:', error);
      },
      complete: () => {
        console.log('Fetching products completed.');
      },
    });
  }

  getPages(): number[] {
    return [...Array(this.pagination?.totalPages!).keys()].map((i) => i + 1);
  }

  deleteProduct(id: number) {
    console.log('Deleting product with id:', id);
    this.productService.deleteProduct(id).subscribe({
      next: (response) => {
        console.log('Product deleted successfully:', response);
        this.onFilter();
      },
      error: (error) => {
        console.error('Error deleting product:', error);
      },
      complete: () => {
        console.log('Deleting product completed.');
      },
    });
  }

  handlePage(event: PageEvent) {
    const pageIndex = event.pageIndex;
    const pageSize = event.pageSize;
    this.changePage(pageIndex + 1);
  }

  changePage(page: number) {
    if (page >= 1 && page <= this.pagination?.totalPages!) {
      this.pagination!.currentPage = page;
      this.onFilter(page);
    }
  }
}
