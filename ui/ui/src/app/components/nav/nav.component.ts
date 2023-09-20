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

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent {
  constructor(public authService: AuthService, private router: Router) {
    library.add(faHome, faBoxes, faSignInAlt, faUserPlus, faStore, faGithub);
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/home');
  }
}
