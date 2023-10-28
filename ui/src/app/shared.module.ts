import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FontAwesomeModule,
  FaIconLibrary,
} from '@fortawesome/angular-fontawesome';
import {
  faHome,
  faBoxes,
  faSignInAlt,
  faUserPlus,
  faStore,
  faSignOutAlt,
  faUser,
  faPencilAlt,
  faTrashAlt,
  faPlusSquare,
  faLock,
  faEdit,
  faStar,
  faShoppingCart,
  faPlus,
  faMinus,
  faTrash,
  faUserShield,
} from '@fortawesome/free-solid-svg-icons';
import { NoAutocompleteDirective } from './directives/no-autocomplete.directive'; // Ensure the path is correct
import { HasRoleDirective } from './directives/hasrole.directive'; // Ensure the path is correct
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { faGithub } from '@fortawesome/free-brands-svg-icons';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';

@NgModule({
  declarations: [NoAutocompleteDirective, HasRoleDirective],
  imports: [CommonModule, FontAwesomeModule, FormsModule, ReactiveFormsModule],
  exports: [
    CommonModule,
    FontAwesomeModule,
    NoAutocompleteDirective,
    HasRoleDirective,
    FormsModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    MatTableModule,
    MatTabsModule,
    MatPaginatorModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatFormFieldModule,
  ],
})
export class SharedModule {
  constructor(library: FaIconLibrary) {
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
      faLock,
      faEdit,
      faStar,
      faShoppingCart,
      faPlus,
      faMinus,
      faTrash,
      faUserShield
    );
  }
}
