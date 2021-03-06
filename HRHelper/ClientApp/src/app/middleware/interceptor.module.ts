import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Injectable, NgModule } from "@angular/core";
import { Observable } from "rxjs";


@Injectable()
class HttpsRequestInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    var _user = JSON.parse(localStorage.getItem('loginData'));

    const duplicatedRequest = req.clone({
      headers: req.headers.set('authorization', (_user && _user.token) ? 'Bearer ' + _user.token : '')
    });
    return next.handle(duplicatedRequest);
    }

}


@NgModule({
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpsRequestInterceptor,
      multi: true,
    }
  ]
})

export class Interceptor { }
