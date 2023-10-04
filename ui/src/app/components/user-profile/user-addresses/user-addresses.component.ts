import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Output,
  SimpleChanges,
} from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserAddress } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-addresses',
  templateUrl: './user-addresses.component.html',
  styleUrls: ['./user-addresses.component.scss'],
})
export class UserAddressesComponent {
  @Input() addresses: UserAddress[] | undefined;
  @Output() mainAddressId: number | undefined;
  @Output() addressToDelete = new EventEmitter<number>();
  @Output() addressToEdit = new EventEmitter<number>();

  constructor(
    private authService: AuthService,
    private toastr: ToastrService
  ) {}
  ngOnInit() {
    if (this.addresses) {
      this.mainAddressId = this.addresses.find((address) => address.isMain)?.id;
      console.log('Initial mainAddressId:', this.mainAddressId);
    }
  }
  emitDeleteEvent(id: number) {
    this.addressToDelete.emit(id);
  }

  emitUpdateEvent(id: number) {
    this.addressToEdit.emit(id);
  }

  changeMainAddress() {
    this.emitUpdateEvent(this.mainAddressId!);
    // console.log('Endereço principal alterado:', this.mainAddressId);
    // if (this.mainAddressId) {
    //   this.authService.setMainAdress(this.mainAddressId!).subscribe({
    //     next: (response) => {
    //       if (response.status === 200) {
    //         this.toastr.success('Endereço principal alterado com sucesso.');
    //       }
    //     },
    //     error: (error) => {
    //       if (error.status === 200) {
    //         this.toastr.success('Endereço principal alterado com sucesso.');
    //       } else {
    //         this.toastr.info('Alguma coisa aconteceu de errado');
    //       }
    //     },
    //   });
    // }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['addresses'] && changes['addresses'].currentValue) {
      console.log('Endereços recebidos:', this.addresses);
      this.mainAddressId = this.addresses!.find(
        (address) => address.isMain
      )?.id;
    }
  }

  radioChanged(event: any, id: number) {
    if (event.target.checked) {
      this.mainAddressId = id;
      console.log('Endereço selecionado:', this.mainAddressId);
    }
  }
}
