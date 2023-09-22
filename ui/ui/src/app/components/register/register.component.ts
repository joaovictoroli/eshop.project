import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';
import { CustomValidators } from 'src/app/staticClassses/validation';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  registerUserForm: FormGroup = new FormGroup({});
  constructor(
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.registerUserForm = new FormGroup({
      username: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(12),
        CustomValidators.noSpecialCharacters,
      ]),
      knownAs: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(12),
        CustomValidators.noSpecialCharacters,
      ]),
      password: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(12),
        CustomValidators.noSpecialCharacters,
      ]),
      confirmPassword: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(12),
        CustomValidators.noSpecialCharacters,
        CustomValidators.matchValues('password'),
      ]),
    });
    this.registerUserForm.controls['password'].valueChanges.subscribe({
      next: () =>
        this.registerUserForm.controls[
          'confirmPassword'
        ].updateValueAndValidity(),
    });
  }

  Submit() {
    console.log('submit');
    var form = this.registerUserForm.value;
    console.log(form);
    this.authService.registerUser(this.registerUserForm.value).subscribe({
      next: (response) => {
        console.log(response);
        if (response.body) {
          this.authService.setCurrentUser(response.body);
          this.router.navigateByUrl('/profile');
        }
      },
      error: (error) => {
        this.toastr.error(error.title, error.error);
      },
    });
  }
}
