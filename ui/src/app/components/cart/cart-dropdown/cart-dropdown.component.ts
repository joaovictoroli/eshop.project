import { Component } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-cart-dropdown',
  templateUrl: './cart-dropdown.component.html',
  styleUrls: ['./cart-dropdown.component.scss'],
})
export class CartDropdownComponent {
  totalItems$ = this.cartService.totalItems$;
  cartItems$ = this.cartService.cartItems$;
  totalPrice$ = this.cartService.totalPrice$;

  constructor(private cartService: CartService) {
    this.cartItems$.subscribe((items) => console.log(items));
    this.totalItems$.subscribe((totalItems) => console.log(totalItems));
    this.totalPrice$.subscribe((totalPrice) => console.log(totalPrice));
  }
}
