import { Component, Input, Output } from '@angular/core';
import { NgModel } from '@angular/forms';
import { UserOrder } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-orders',
  templateUrl: './user-orders.component.html',
  styleUrls: ['./user-orders.component.scss'],
})
export class UserOrdersComponent {
  @Input() Orders: UserOrder[] = [];

  constructor(private authService: AuthService) {
    this.authService.currentUser$.subscribe((user) => {
      if (user) {
        this.Orders = user.orders;
      }
    });
  }
}
