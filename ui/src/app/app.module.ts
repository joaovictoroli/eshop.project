import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './components/nav/nav.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ProfileComponent } from './components/user-profile/profile/profile.component';
import { ListProductComponent } from './components/products/list-product/list-product.component';
import { HomeComponent } from './components/home/home.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FooterComponent } from './components/footer/footer.component';
import { CardProductComponent } from './components/products/card-product/card-product.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { UserAddressesComponent } from './components/user-profile/user-addresses/user-addresses.component';
import { FormValidationComponent } from './components/form-validation/form-validation.component';
import { DetailedProductComponent } from './components/products/detailed-product/detailed-product.component';
import { RegisterAddressModalComponent } from './components/user-profile/register-address-modal/register-address-modal.component';
import { CartDropdownComponent } from './components/cart/cart-dropdown/cart-dropdown.component';
import { NgbDropdownModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OrderCheckoutComponent } from './components/order-checkout/order-checkout.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { ListProductsComponent } from './components/admin-panel/products/list-products.component';
import { ListCategoriesComponent } from './components/admin-panel/categories/list-categories.component';
import { ListSubcategoriesComponent } from './components/admin-panel/subcategories/list-subcategories.component';
import { SharedModule } from './shared.module';
import { UserOrdersComponent } from './components/user-profile/user-orders/user-orders.component';
import { UnauthorizedInterceptor } from './interceptors/unauthorized.interceptor';
import { BadrequestInterceptor } from './interceptors/badrequest.interceptor';
import { AddCategoryComponent } from './components/admin-panel/add-category/add-category.component';
import { AddSubcategoryComponent } from './components/admin-panel/add-subcategory/add-subcategory.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    ListProductComponent,
    HomeComponent,
    FooterComponent,
    CardProductComponent,
    UserAddressesComponent,
    RegisterAddressModalComponent,
    FormValidationComponent,
    DetailedProductComponent,
    CartDropdownComponent,
    OrderCheckoutComponent,
    AdminPanelComponent,
    ListProductsComponent,
    ListCategoriesComponent,
    ListSubcategoriesComponent,
    UserOrdersComponent,
    AddCategoryComponent,
    AddSubcategoryComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    NgbModule,
    NgbDropdownModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: UnauthorizedInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BadrequestInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
