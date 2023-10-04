import { Component } from '@angular/core';
import {
  faHome,
  faBoxes,
  faSignInAlt,
  faUserPlus,
  faStore,
} from '@fortawesome/free-solid-svg-icons';
import { faGithub } from '@fortawesome/free-brands-svg-icons';
import { library } from '@fortawesome/fontawesome-svg-core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { shareReplay } from 'rxjs/internal/operators/shareReplay';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent {
  totalItems$ = this.shoppingCartService.totalItems$;

  constructor(
    public authService: AuthService,
    private router: Router,
    private shoppingCartService: CartService
  ) {
    library.add(faHome, faBoxes, faSignInAlt, faUserPlus, faStore, faGithub);
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/home');
  }
}
