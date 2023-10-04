import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { User, UserAddress } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { CartItem } from 'src/app/models/cartItem';

@Component({
  selector: 'app-order-checkout',
  templateUrl: './order-checkout.component.html',
  styleUrls: ['./order-checkout.component.scss'],
})
export class OrderCheckoutComponent implements OnInit {
  cartItems$ = this.cartService.cartItems$;
  totalItems$ = this.cartService.totalItems$;
  totalPrice$ = this.cartService.totalPrice$;
  user: User | null | undefined;
  mainAdress: UserAddress | null | undefined;

  constructor(
    private cartService: CartService,
    private authService: AuthService
  ) {
    this.authService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => (this.user = user),
    });
  }

  ngOnInit() {
    this.loadAuthorizedUser();
  }

  loadAuthorizedUser() {
    if (this.user && this.user.username) {
      this.authService.getUserProfile(this.user.username).subscribe({
        next: (user) => {
          this.user = user;
          this.mainAdress = this.user.addresses.find(
            (address) => address.isMain
          );
          console.log(user.addresses);
        },
        error: (error) => console.error('Error fetching user:', error),
      });
    }
  }
  increaseQuantity(item: CartItem) {
    this.cartService.addItem(item.product);
  }

  decreaseQuantity(item: CartItem) {
    this.cartService.removeItem(item.product);
  }

  deleteItem(item: CartItem) {
    this.cartService.deleteProduct(item.product);
  }
}
