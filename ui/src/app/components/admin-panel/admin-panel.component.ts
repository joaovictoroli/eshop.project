import { Component } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { Category } from 'src/app/models/categories';
import { Product } from 'src/app/models/product';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.scss'],
})
export class AdminPanelComponent {
  categories: Category[] = [];
  products: Product[] = [];
  private unsubscribe$ = new Subject<void>();

  constructor(private categoryService: CategoryService) {}
  ngOnInit() {
    this.categoryService.getCategoriesNSubCategories().subscribe((data) => {
      this.categories = data;
    });

    this.categoryService.updateObservable$
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(() => {
        this.fetchCategories();
      });
  }

  fetchCategories() {
    this.categoryService.getCategoriesNSubCategories().subscribe((data) => {
      this.categories = data;
    });
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
