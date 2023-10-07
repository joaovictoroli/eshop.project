import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { User, UserAddress } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { CartItem } from 'src/app/models/cartItem';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/services/order.service';
import { Order } from 'src/app/models/order';
import { Router } from '@angular/router';

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
    private authService: AuthService,
    private toastr: ToastrService,
    private orderService: OrderService,
    private router: Router
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

  confirmPurchase() {
    this.cartItems$.pipe(take(1)).subscribe((cartItems) => {
      const orderData: Order = {
        orderProducts: cartItems.map((item) => ({
          productName: item.product.name,
          quantity: item.quantity,
        })),
      };

      if (orderData.orderProducts.length === 0) {
        this.toastr.error('Adicione produtos ao carrinho antes de finalizar.');
        return;
      }

      this.orderService.createOrder(orderData).subscribe(
        (response) => {
          this.toastr.success('Pedido realizado com sucesso.');
          this.cartService.clearCart();
          this.router.navigate(['/profile']);
          console.log(response);
        },
        (error) => {
          this.toastr.error(error.errorMessage);
          console.error('Error placing order:', error);
        }
      );
    });
  }

  deleteAddress(id: number) {
    this.authService.deleteAddress(id).subscribe({
      next: (response) => {
        if (response.status === 204) {
          this.loadAuthorizedUser();
          this.toastr.success('Endereço deletado com sucesso.');
        }
      },
      error: (error) => {
        console.error('Error deleting address:', error);
        if (error.status === 400) {
          const errorMessage = error.error;
          this.toastr.error(errorMessage);
        }
      },
    });
  }

  addressEdit(mainAddressId: number) {
    console.log('Endereço principal alterado:', mainAddressId);
    if (mainAddressId) {
      this.authService.setMainAdress(mainAddressId!).subscribe({
        next: (response) => {
          if (response.status === 200) {
            console.log('padrao no content');
          }
        },
        error: (error) => {
          if (error.status === 200) {
            this.toastr.success('Endereço principal alterado com sucesso.');
            this.loadAuthorizedUser();
          } else {
            this.toastr.info('Alguma coisa aconteceu de errado');
          }
        },
      });
    }
  }
}
