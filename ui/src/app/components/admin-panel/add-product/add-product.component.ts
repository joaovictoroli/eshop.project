import { Component, Input, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category, SubCategory } from 'src/app/models/categories';
import { ProductService } from 'src/app/services/product.service';
import { CustomValidators } from 'src/app/staticClassses/validation';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss'],
})
export class AddProductComponent {
  @Input() categories: Category[] = [];
  subCategories: SubCategory[] = [];
  addProductForm: FormGroup;

  constructor(private fb: FormBuilder, private productService: ProductService) {
    this.addProductForm = this.fb.group({
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
      price: ['', [Validators.required, CustomValidators.decimalNumber()]],
      technicalInfo: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          CustomValidators.noSpecialCharactersExceptSpace,
        ],
      ],
      subCategoryName: ['', [Validators.required]],
      image: ['', [Validators.required]],
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['categories'] && changes['categories'].currentValue) {
      this.updateSubCategories();
    }
  }

  onSubmit() {
    if (this.addProductForm.valid) {
      const formData = new FormData();
      const formValue = this.addProductForm.value;

      for (const key in formValue) {
        if (key !== 'image') {
          formData.append(key, formValue[key]);
        } else {
          // Append the file to FormData
          const file: File = formValue[key];
          formData.append('File', file, file.name);
        }
      }

      this.productService.addProduct(formData).subscribe(
        (res) => {
          console.log('Produto adicionado com sucesso:', res);
          this.addProductForm.reset();
        },
        (error) => {
          console.error('Erro ao adicionar produto:', error);
        }
      );
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

  onFileChange(event: any) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      this.addProductForm.patchValue({
        image: file,
      });
    }
  }
}
