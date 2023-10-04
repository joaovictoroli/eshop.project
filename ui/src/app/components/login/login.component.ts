import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  model: any = {};

  constructor(public authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.model).subscribe({
      next: (_) => {
        this.model = {};
        console.log('Logged in successfully');
        this.router.navigateByUrl('/profile');
      },
    });
  }

  logout() {
    this.authService.logout();
  }
}
