import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddCategory } from 'src/app/models/categories';
import { CategoryService } from 'src/app/services/category.service';
import { CustomValidators } from 'src/app/staticClassses/validation';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.scss'],
})
export class AddCategoryComponent {
  categoryForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private router: Router
  ) {
    this.categoryForm = this.fb.group({
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
    });
  }

  onSubmit() {
    if (this.categoryForm.valid) {
      let addCategory: AddCategory | undefined = this.categoryForm.value;
      if (addCategory) {
        this.categoryService.addCategory(addCategory).subscribe((res) => {
          if (res.status == 200) {
            this.categoryService.notifyUpdate();
            this.categoryForm.reset();
          }
        });
        console.log(this.categoryForm.value);
      }
    }
  }
}
