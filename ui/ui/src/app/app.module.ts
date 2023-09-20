import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './components/nav/nav.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ListProductComponent } from './components/products/list-product/list-product.component';
import { DetailedProductComponent } from './components/products/detailed-product/detailed-product.component';
import { HomeComponent } from './components/home/home.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NoAutocompleteDirective } from './directives/no-autocomplete.directive';
import { FooterComponent } from './components/footer/footer.component';
import { library } from '@fortawesome/fontawesome-svg-core';
import {
  faSignOutAlt,
  faUser,
  faPencilAlt,
  faTrashAlt,
  faPlusSquare,
  faHome,
  faBoxes,
  faSignInAlt,
  faUserPlus,
  faStore,
  faLock,
  faEdit,
  faStar,
} from '@fortawesome/free-solid-svg-icons';
import { faGithub } from '@fortawesome/free-brands-svg-icons';
import {
  FaIconLibrary,
  FontAwesomeModule,
} from '@fortawesome/angular-fontawesome';
import { CardProductComponent } from './components/products/card-product/card-product.component';
import { FormsModule } from '@angular/forms';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    ListProductComponent,
    DetailedProductComponent,
    HomeComponent,
    NoAutocompleteDirective,
    FooterComponent,
    CardProductComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CommonModule,
    ToastrModule.forRoot(),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
  constructor(library: FaIconLibrary) {
    // Add an icon to the library for convenient access in other components
    library.addIcons(
      faHome,
      faBoxes,
      faSignInAlt,
      faUserPlus,
      faStore,
      faGithub,
      faSignOutAlt,
      faUser,
      faPencilAlt,
      faTrashAlt,
      faPlusSquare,
      faHome,
      faLock,
      faEdit,
      faStar
    );
  }
}
