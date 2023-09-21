import { HttpClient } from '@angular/common/http';
import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  ViewChild,
} from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ValidatorFn,
  AbstractControl,
  ValidationErrors,
  FormControl,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AddressToRegister } from 'src/app/models/user';
import { AuthService, RegisterAddress } from 'src/app/services/auth.service';

declare var bootstrap: any; // Bootstrap 5

@Component({
  selector: 'app-register-address-modal',
  templateUrl: './register-address-modal.component.html',
  styleUrls: ['./register-address-modal.component.scss'],
})
export class RegisterAddressModalComponent {
  cepForm: FormGroup = new FormGroup({});
  aditionalForm: FormGroup = new FormGroup({});
  dataFetched = new dataFetched();

  isCEPFetched: boolean = false;

  constructor(
    private http: HttpClient,
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService,
    private cdRef: ChangeDetectorRef
  ) {
    this.cepForm = new FormGroup({
      cep: new FormControl<string>('', [
        Validators.required,
        this.eightDigitsPatternValidator(),
      ]),
    });

    this.aditionalForm = new FormGroup({
      numero: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(5),
        this.numericPatternValidator(),
      ]),
      infoAdicional: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(20),
        this.noSpecialCharactersValidator(),
      ]),
      apartamento: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(5),
        this.numericPatternValidator(),
      ]),
    });
  }

  eightDigitsPatternValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const valid = /^\d{8}$/.test(control.value);
      return valid ? null : { notEightDigits: true };
    };
  }

  numericPatternValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const valid = /^\d*$/.test(control.value);
      return valid ? null : { notNumeric: true };
    };
  }

  noSpecialCharactersValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (control.value) {
        const valid = /^[a-zA-Z0-9 ]+$/.test(control.value);
        return valid ? null : { specialCharactersFound: true };
      }
      return null;
    };
  }

  onSubmit() {
    var registerAdress = new AddressToRegister({
      ...this.cepForm.value,
      ...this.aditionalForm.value,
    });

    var cep = this.cepForm.value.cep;
    this.authService.registerAddress(registerAdress, cep).subscribe(
      (response) => {
        if (response.status === 200) {
          this.toastr.success('Endereço adicionado com sucesso.');
          this.router.navigate(['/profile']);
        }
      },
      (error) => {
        this.toastr.error('Erro ao registrar endereço.');
      }
    );
  }

  fetchCEPDetails() {
    if (this.cepForm.valid) {
      this.http
        .get<any>(`https://viacep.com.br/ws/${this.cepForm.value.cep}/json/`)
        .subscribe(
          (response) => {
            if (response && response.erro !== true) {
              this.dataFetched.cep = response.cep;
              this.dataFetched.complemento = response.complemento;
              this.dataFetched.bairro = response.bairro;
              this.dataFetched.uf = response.uf;
              console.log('Endereço ' + this.dataFetched);

              if (this.dataFetched) {
                console.log(this.dataFetched);
                console.log(this.dataFetched.cep);
                this.isCEPFetched = true;
                this.toastr.success('CEP encontrado');
              }
            } else {
              this.toastr.error('CEP não encontrado');
            }
          },
          (error) => {
            this.toastr.error('Erro ao buscar CEP.');
          }
        );
    } else {
      this.toastr.warning('CEP inválido. Insira um CEP válido para continuar.');
    }
  }
}

export class dataFetched {
  cep: string = '';
  complemento: string = '';
  bairro: string = '';
  uf: string = '';
}
