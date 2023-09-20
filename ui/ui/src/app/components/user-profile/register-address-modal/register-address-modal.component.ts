import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-register-address-modal',
  templateUrl: './register-address-modal.component.html',
  styleUrls: ['./register-address-modal.component.scss'],
})
export class RegisterAddressModalComponent {
  @Input()
  address = {
    cep: '',
    uf: '',
    bairro: '',
    // ... outros campos do endereço
  };

  onSubmit() {
    console.log(this.address);
  }
}
