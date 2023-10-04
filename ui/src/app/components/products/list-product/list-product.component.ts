import { HttpStatusCode } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { Category } from 'src/app/models/categories';
import { Pagination, QueryParams } from 'src/app/models/pagination';
import { Product } from 'src/app/models/product';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-list-product',
  templateUrl: './list-product.component.html',
  styleUrls: ['./list-product.component.scss'],
})
export class ListProductComponent {
  categories: Category[] | undefined;
  products: Product[] | undefined;
  queryParams: QueryParams = new QueryParams();
  pagination: Pagination | undefined;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private cd: ChangeDetectorRef
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

  changePage(page: number) {
    if (page >= 1 && page <= this.pagination?.totalPages!) {
      this.pagination!.currentPage = page;
      this.onFilter(page);
    }
  }

  getPages(): number[] {
    return [...Array(this.pagination?.totalPages!).keys()].map((i) => i + 1);
  }
}
