import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      tap((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          const message =
            event.body?.message || 'Operação realizada com sucesso!';
          this.snackBar.open(message, 'Fechar', {
            duration: 3000,
            panelClass: ['mat-toolbar', 'mat-primary'],
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
          });
        }
      }),
      catchError((err: any) => {
        if (err.status === 401) {
          this.snackBar.open(
            `Error: ${err.error.message || err.error || 'Unknown error'}`,
            'Fechar',
            {
              duration: 3000,
              panelClass: ['mat-toolbar', 'mat-warn'],
              verticalPosition: 'bottom',
              horizontalPosition: 'center',
            }
          );
          this.authService.logout();
        } else if (err.status === 403) {
          console.log('never');
        }
        return throwError(err);
      })
    );
  }
}

// private showSnackBar(err: any) {
//   let errorMessage = err.error.message || err.error || 'Unknown error';
//   this.snackBar.open(`Error: ${errorMessage}`, 'Fechar', {
//     duration: 3000,
//     panelClass: ['mat-toolbar', 'mat-warn'],
//     verticalPosition: 'bottom',
//     horizontalPosition: 'center',
//   });
// }
