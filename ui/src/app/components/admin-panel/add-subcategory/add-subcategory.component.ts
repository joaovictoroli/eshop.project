import { Component, Input, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddSubCategory, Category } from 'src/app/models/categories';
import { CategoryService } from 'src/app/services/category.service';
import { CustomValidators } from 'src/app/staticClassses/validation';

@Component({
  selector: 'app-add-subcategory',
  templateUrl: './add-subcategory.component.html',
  styleUrls: ['./add-subcategory.component.scss'],
})
export class AddSubcategoryComponent {
  @Input() categories: Category[] = [];
  subCategoryForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService
  ) {
    this.subCategoryForm = this.fb.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          CustomValidators.noSpecialCharactersExceptSpace,
        ],
      ],
      description: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          CustomValidators.noSpecialCharactersExceptSpace,
        ],
      ],
      categoryId: ['', Validators.required],
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['categories'] && changes['categories'].currentValue) {
      this.updateCategories();
    }
  }

  updateCategories() {
    console.log('Este é um log de teste!');
  }

  onSubmit() {
    if (this.subCategoryForm.valid) {
      let addCategory: AddSubCategory | undefined = this.subCategoryForm.value;
      if (addCategory) {
        console.log(this.subCategoryForm.value);
        this.categoryService.addSubCategory(addCategory).subscribe((data) => {
          console.log(data.status);
          if (data.status === 200) {
            this.categoryService.notifyUpdate();
            this.subCategoryForm.reset();
          }
        });
      }
    }
  }
}
