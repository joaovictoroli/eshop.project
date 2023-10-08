import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class BadrequestInterceptor implements HttpInterceptor {
  constructor(private snackBar: MatSnackBar) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 400) {
          console.log('Bad Request Interceptor');
          let errorMessage =
            error.error.message || error.error || 'Unknown error';
          this.snackBar.open(`Error: ${errorMessage}`, 'Fechar', {
            duration: 3000,
            panelClass: ['mat-toolbar', 'mat-warn'],
            verticalPosition: 'bottom',
            horizontalPosition: 'center',
          });
        }
        return throwError(error);
      })
    );
  }
}
