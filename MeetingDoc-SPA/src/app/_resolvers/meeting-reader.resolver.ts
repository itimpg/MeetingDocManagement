import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { MeetingContentService } from '../_services/meeting-content.service';
import { MeetingContent } from '../_models/MeetingContent';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class MeetingReaderResolver implements Resolve<MeetingContent[]> {
  protected pageNumber = 1;
  protected pageSize = 1;

  constructor(
    protected service: MeetingContentService,
    protected router: Router,
    protected alertify: AlertifyService
  ) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<MeetingContent[]> {
    return this.service
      .getContents(route.params['agendaId'], this.pageNumber, this.pageSize)
      .pipe(
        catchError(eror => {
          this.alertify.error('Problem retrieving data');
          this.router.navigate(['']);
          return of(null);
        })
      );
  }
}
