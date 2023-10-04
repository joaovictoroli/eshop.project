import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Components
import { ListProductComponent } from './components/products/list-product/list-product.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ProfileComponent } from './components/user-profile/profile/profile.component';
import { UserAddressesComponent } from './components/user-profile/user-addresses/user-addresses.component';
import { RegisterAddressModalComponent } from './components/user-profile/register-address-modal/register-address-modal.component';
import { DetailedProductComponent } from './components/products/detailed-product/detailed-product.component';
import { OrderCheckoutComponent } from './components/order-checkout/order-checkout.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';

// Guards
import { authGuard } from './guards/auth.guard';
import { adminGuard } from './guards/admin.guard';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'products', component: ListProductComponent },
  { path: 'product/:name', component: DetailedProductComponent },
  {
    path: '',
    children: [
      {
        path: 'login',
        component: LoginComponent,
        canActivate: [authGuard],
        data: { isGuest: true },
      },
      {
        path: 'register',
        component: RegisterComponent,
        canActivate: [authGuard],
        data: { isGuest: true },
      },
      {
        path: 'order-checkout',
        component: OrderCheckoutComponent,
        canActivate: [authGuard],
        data: { isAuth: true },
      },
    ],
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [authGuard],
    data: { isAuth: true },
    children: [
      { path: 'addresses', component: UserAddressesComponent },
      { path: 'add-address', component: RegisterAddressModalComponent },
    ],
  },
  {
    path: 'admin-panel',
    component: AdminPanelComponent,
    canActivate: [adminGuard],
  },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
