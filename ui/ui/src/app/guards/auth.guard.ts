import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Observable, map } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const isUserLoggedIn = this.authService.isUserLoggedIn();
    console.log('isUserLoggedIn', isUserLoggedIn);
    if (route.data['isGuest'] && isUserLoggedIn) {
      this.router.navigate(['/profile']);
      return false;
    }

    // Caso a rota exija que o usuário esteja logado
    if (route.data['isAuth'] && !isUserLoggedIn) {
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }
}
