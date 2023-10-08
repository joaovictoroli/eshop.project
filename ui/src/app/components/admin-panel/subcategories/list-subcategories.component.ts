import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { Category, SubCategory } from 'src/app/models/categories';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-list-subcategories',
  templateUrl: './list-subcategories.component.html',
  styleUrls: ['./list-subcategories.component.scss'],
})
export class ListSubcategoriesComponent implements OnChanges {
  @Input() categories: Category[] = [];
  subCategories: SubCategory[] = [];

  constructor(private categoryService: CategoryService) {}

  ngOnChanges(changes: SimpleChanges) {
    if (changes['categories'] && changes['categories'].currentValue) {
      this.updateSubCategories();
    }
  }

  updateSubCategories() {
    console.log('categories', this.categories);
    this.subCategories = [];
    this.categories.forEach((category) => {
      if (category.subCategories) {
        this.subCategories = [...this.subCategories, ...category.subCategories];
        console.log('subCategories', this.subCategories);
      }
    });
    console.log('Este é um log de teste!');
  }

  onDelete(subCategory: SubCategory) {
    this.categoryService.deleteSubCategory(subCategory.id).subscribe((res) => {
      if (res.status === 204) {
        this.categoryService.notifyUpdate();
      }
    });
  }
}
