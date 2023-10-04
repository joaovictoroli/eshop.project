import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Product } from '../models/product';
import { CartItem } from '../models/cartItem';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private _cartItems = new BehaviorSubject<CartItem[]>([]);
  private _totalItems = new BehaviorSubject<number>(0);
  private _totalPrice = new BehaviorSubject<number>(0);

  get cartItems$() {
    return this._cartItems.asObservable();
  }

  get totalItems$() {
    return this._totalItems.asObservable();
  }
  get totalPrice$() {
    return this._totalPrice.asObservable();
  }

  private updateTotalItems(items: CartItem[]) {
    const totalItems = items.reduce((acc, item) => acc + item.quantity, 0);
    this._totalItems.next(totalItems);

    const totalPrice = items.reduce(
      (acc, item) => acc + item.product.price * item.quantity,
      0
    );
    this._totalPrice.next(parseFloat(totalPrice.toFixed(2)));
  }

  addItem(product: Product) {
    const currentItems = this._cartItems.getValue();
    const itemIndex = currentItems.findIndex(
      (item) => item.product.name === product.name
    ); // considerando que 'Product' tem uma propriedade 'id'

    if (itemIndex !== -1) {
      // O produto já está no carrinho, então apenas aumente a quantidade
      currentItems[itemIndex].quantity++;
    } else {
      // Adicione o novo produto ao carrinho
      currentItems.push({ product, quantity: 1 });
    }

    this._cartItems.next(currentItems);
    this.updateTotalItems(currentItems);
  }

  removeItem(product: Product) {
    const currentItems = this._cartItems.getValue();
    const itemIndex = currentItems.findIndex(
      (item) => item.product.name === product.name
    );

    if (itemIndex !== -1) {
      if (currentItems[itemIndex].quantity > 1) {
        currentItems[itemIndex].quantity--;
      } else {
        currentItems.splice(itemIndex, 1);
      }
      this._cartItems.next(currentItems);
      this.updateTotalItems(currentItems);
    }
  }

  deleteProduct(product: Product) {
    const currentItems = this._cartItems.getValue();
    const itemIndex = currentItems.findIndex(
      (item) => item.product.name === product.name
    );

    if (itemIndex !== -1) {
      currentItems.splice(itemIndex, 1);
      this._cartItems.next(currentItems);
      this.updateTotalItems(currentItems);
    }
  }
}
