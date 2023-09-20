import {
  Component,
  EventEmitter,
  Input,
  Output,
  SimpleChanges,
} from '@angular/core';
import { UserAddress } from 'src/app/models/user';

@Component({
  selector: 'app-user-addresses',
  templateUrl: './user-addresses.component.html',
  styleUrls: ['./user-addresses.component.scss'],
})
export class UserAddressesComponent {
  @Input() addresses: UserAddress[] | undefined;
  mainAddressId: number | undefined;
  @Output() addressToDelete = new EventEmitter<number>();

  ngOnInit() {
    if (this.addresses) {
      this.mainAddressId = this.addresses.find((address) => address.isMain)?.id;
      console.log('Initial mainAddressId:', this.mainAddressId);
    }
    console.log('Initial addresses:', this.addresses);
  }
  emitDeleteEvent(id: number) {
    this.addressToDelete.emit(id);
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['addresses'] && changes['addresses'].currentValue) {
      console.log('Endereços recebidos:', this.addresses);
      this.mainAddressId = this.addresses!.find(
        (address) => address.isMain
      )?.id;
      console.log('Main Address ID:', this.mainAddressId);
    }
  }
}
