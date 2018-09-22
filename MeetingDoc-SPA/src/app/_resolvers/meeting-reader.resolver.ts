import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { MeetingAgenda } from '../_models/MeetingAgenda';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MeetingScheduleService } from '../_services/meeting-schedule.service';

@Injectable()
export class MeetingReaderResolver
  implements Resolve<MeetingAgenda[]> {
  protected pageNumber = 1;
  protected pageSize = 5;

  constructor(
    protected service: MeetingScheduleService,
    protected router: Router,
    protected alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<MeetingAgenda[]> {
    return this.service
      .getAgendas(route.params['id'], this.pageNumber, this.pageSize)
      .pipe(
        catchError(eror => {
          this.alertify.error('Problem retrieving data');
          this.router.navigate(['']);
          return of(null);
        })
      );
  }
}
