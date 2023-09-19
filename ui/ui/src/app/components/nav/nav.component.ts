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

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent {
  constructor() {
    library.add(faHome, faBoxes, faSignInAlt, faUserPlus, faStore, faGithub);
  }
}
