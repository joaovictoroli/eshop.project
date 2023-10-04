import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AuthService);
  const router = inject(Router);

  return accountService.currentUser$.pipe(
    map((user) => {
      if (!user) {
        router.navigate(['/login']);
        return false;
      }
      if (user!.roles.includes('Admin')) {
        console.log('Admin');
        return true;
      } else {
        router.navigate(['/login']);
        return false;
      }
    })
  );
};
