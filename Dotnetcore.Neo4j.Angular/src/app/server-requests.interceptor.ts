import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpResponse, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { HandleErrorService } from './services/handle-error.service';

/**
 * Injectable interceptor to intercept http requests and add authentication headers and also add generic http api calls error handling
 */
@Injectable()
export class ServerRequestInterceptor implements HttpInterceptor {

  constructor(private errorHandler: HandleErrorService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    req = req.clone({ withCredentials: true });

    return new Observable((observer) => {
      // subscribing to requests
      next.handle(req).subscribe({
        next: (res: HttpEvent<any>) => {
          if(res instanceof HttpResponse)
          {
            // Continuouing HTTP Cycle
            observer.next(res);
          }
        },
        error: (err: HttpErrorResponse) => {
          // Handling Errors
          this.errorHandler.handleError(err);
          observer.error(err);
        }
      });
    });
  }
}