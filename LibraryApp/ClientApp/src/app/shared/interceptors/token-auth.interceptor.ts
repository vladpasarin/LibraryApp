import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import {ToastrService} from 'ngx-toastr';
import { tap } from 'rxjs/operators';


@Injectable()
export class TokenAuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService,
    private toastService: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const hardcodedToken = '1d38d128-0671-4121-8084-f6332a992a40';
    request = request.clone({
      setHeaders: {
        Authorization: 'Bearer ${hardcodedToken}'
      }
    });

    return next.handle(request).pipe(tap(() => {
    },
    (errorObject: any) => {
      if (errorObject instanceof HttpErrorResponse) {
        if (errorObject.status === 401) {
          this.authService.isLoggedIn();
        } else if (errorObject instanceof HttpErrorResponse && errorObject.status === 400) {
          const errorMessage = typeof errorObject.error === 'string' ? errorObject.error : 'Something went wrong...';
          this.toastService.error(errorMessage);
        } else {
          this.toastService.error('Something went wrong...');
        }
      }
    }));
  }
}
