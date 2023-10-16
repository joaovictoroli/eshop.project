import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from 'src/app/staticClassses/validation';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss'],
})
export class AddProductComponent {
  addProductForm: FormGroup;

  constructor(private fb: FormBuilder) {
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
      image: ['', [Validators.required]],
    });
  }

  onSubmit() {
    console.log('teste');
  }

  selectedFile: File | null = null;

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }
}
