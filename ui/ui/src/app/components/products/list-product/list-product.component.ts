import { HttpStatusCode } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { Category } from 'src/app/models/categories';
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

  priceMin: number = 0;
  priceMax: number = 0;
  productName: string = '';
  selectedCategory: string = '';

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private cd: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.categoryService.getCategoriesNSubCategories().subscribe({
      next: (value) => {
        this.categories = value;
        // console.log(this.categories);
      },
      error: (error) => {
        console.error('Error fetching products:', error);
      },
      complete: () => {
        console.log('Fetching products completed.');
      },
    });

    this.productService.getProductsAllInOne().subscribe({
      next: (value) => {
        this.products = value;
        // console.log(this.products);
      },
      error: (error) => {
        console.error('Error fetching products:', error);
      },
      complete: () => {
        console.log('Fetching products completed.');
      },
    });
  }

  onFilter() {
    console.log({
      priceMin: this.priceMin,
      priceMax: this.priceMax,
      productName: this.productName,
      selectedCategory: this.selectedCategory,
    });
  }
}
