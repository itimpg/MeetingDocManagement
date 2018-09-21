import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from '../_services/base.service';

export abstract class BaseListByParentResolver<T> implements Resolve<T[]> {
  protected pageNumber = 1;
  protected pageSize = 5;

  constructor(
    protected service: BaseService<T>,
    protected router: Router,
    protected alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<T[]> {
    return this.service
      .getItemsByParent(route.params['id'], this.pageNumber, this.pageSize)
      .pipe(
        catchError(eror => {
          this.alertify.error('Problem retrieving data');
          this.router.navigate(['']);
          return of(null);
        })
      );
  }
}
