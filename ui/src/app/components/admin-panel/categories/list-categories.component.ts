import { Component, Input } from '@angular/core';
import { Category, SubCategory } from 'src/app/models/categories';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-list-categories',
  templateUrl: './list-categories.component.html',
  styleUrls: ['./list-categories.component.scss'],
})
export class ListCategoriesComponent {
  constructor(private categoryService: CategoryService) {}
  @Input() categories: Category[] = [];

  ngOnInit() {
    console.log('categories', this.categories);
  }
  onDelete(category: Category) {
    this.categoryService.deleteCategory(category.id).subscribe((res) => {
      if (res.status == 204) {
        this.categoryService.notifyUpdate();
      }
    });
  }
}
