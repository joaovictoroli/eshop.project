import { HttpClient } from '@angular/common/http';
import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnInit,
  EventEmitter,
  Output,
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
import { CustomValidators } from 'src/app/staticClassses/validation';

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
  @ViewChild('fechaModalVinculacao') fechaModalVinculacao!: ElementRef;
  @Output() wasAdded = new EventEmitter<boolean>();

  isCEPFetched: boolean = false;

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private toastr: ToastrService
  ) {
    this.cepForm = new FormGroup({
      cep: new FormControl<string>('', [
        Validators.required,
        CustomValidators.eightDigitsPattern(),
      ]),
    });

    this.aditionalForm = new FormGroup({
      numero: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(5),
        CustomValidators.numericPattern(),
      ]),
      infoAdicional: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(20),
        CustomValidators.noSpecialCharactersExceptSpace(),
      ]),
      apartamento: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(5),
        CustomValidators.numericPattern(),
      ]),
    });
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
          this.emitSuccessfullyAdded();
        }
      },
      (error) => {
        this.toastr.error('Erro ao registrar endereço.');
      }
    );
  }

  emitSuccessfullyAdded() {
    console.log('Endereço adicionado com sucesso.');
    this.wasAdded.emit(true);
    this.closeModal();
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

  closeModal() {
    this.fechaModalVinculacao.nativeElement.click();
  }
}

export class dataFetched {
  cep: string = '';
  complemento: string = '';
  bairro: string = '';
  uf: string = '';
}
