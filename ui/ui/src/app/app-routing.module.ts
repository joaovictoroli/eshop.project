import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListProductComponent } from './components/products/list-product/list-product.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ProfileComponent } from './components/user-profile/profile/profile.component';
import { UserAddressesComponent } from './components/user-profile/user-addresses/user-addresses.component';
import { RegisterAddressModalComponent } from './components/user-profile/register-address-modal/register-address-modal.component';
import { DetailedProductComponent } from './components/products/detailed-product/detailed-product.component';

const routes: Routes = [
  { path: 'products', component: ListProductComponent },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'product/:name', component: DetailedProductComponent },
  {
    path: 'profile',
    component: ProfileComponent,
    children: [
      {
        path: 'addresses',
        component: UserAddressesComponent,
      },
      {
        path: 'add-address',
        component: RegisterAddressModalComponent,
      },
    ],
  },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
