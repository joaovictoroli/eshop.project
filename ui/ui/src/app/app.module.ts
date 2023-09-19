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
import { faGithub } from '@fortawesome/free-brands-svg-icons';
import {
  faHome,
  faBoxes,
  faSignInAlt,
  faUserPlus,
  faStore,
} from '@fortawesome/free-solid-svg-icons';
import {
  FaIconLibrary,
  FontAwesomeModule,
} from '@fortawesome/angular-fontawesome';

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
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule,
  ],
  providers: [],
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
      faGithub
    );
  }
}
