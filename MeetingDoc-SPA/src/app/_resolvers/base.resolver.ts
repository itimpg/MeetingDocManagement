import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from '../_services/base.service';

export abstract class BaseResolver<T> implements Resolve<T> {
  protected abstract action;

  constructor(
    protected service: BaseService<T>,
    protected router: Router,
    protected alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<T> {
    return this.service.getItem(route.params['id']).pipe(
      catchError(eror => {
        this.alertify.error('Problem retrieving data');
        this.router.navigate([`/${this.action}`]);
        return of(null);
      })
    );
  }
}
