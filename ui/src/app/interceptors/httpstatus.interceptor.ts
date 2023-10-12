import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Injectable()
export class HttpStatusInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const excludedRoutes = ['/products'];

    return next.handle(request).pipe(
      tap((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          console.log('URL atual:', this.router.url);
          if (
            !this.router.url.startsWith('/product') &&
            !this.router.url.startsWith('/profile')
          ) {
            const message =
              event.body?.message || 'Operação realizada com sucesso!';
            this.snackBar.open(message, 'Fechar', {
              duration: 3000,
              panelClass: ['mat-toolbar', 'mat-primary'],
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
            });
          }
        }
      }),
      catchError((error: HttpErrorResponse) => {
        let errorMessage =
          error.error.message || error.error || 'Unknown error';
        if (error.status === 401 || error.status === 400) {
          this.snackBar.open(`Error: ${errorMessage}`, 'Fechar', {
            duration: 3000,
            panelClass: ['mat-toolbar', 'mat-warn'],
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
          });

          if (error.status === 401) {
            this.authService.logout();
          }
        } else if (error.status === 403) {
          console.log('never');
        }
        return throwError(error);
      })
    );
  }
}
