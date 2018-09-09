import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from '../_services/base.service';

export abstract class BaseListResolver<T> implements Resolve<T[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(
    protected service: BaseService<T>,
    protected router: Router,
    protected alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<T[]> {
    return this.service.getItems(this.pageNumber, this.pageSize).pipe(
      catchError(error => {
        this.alertify.error('Problem retrieving data');
        this.router.navigate(['/']);
        return of(null);
      })
    );
  }
}
