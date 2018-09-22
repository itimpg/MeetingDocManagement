import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { MeetingSchedule } from '../_models/meeting-schedule';
import { AuthService } from './auth.service';
import { MeetingContentService } from './meeting-content.service';
import { MeetingAgenda } from '../_models/MeetingAgenda';
import { PaginatedResult } from '../_models/pagination';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MeetingScheduleService extends BaseService<MeetingSchedule> {
  protected parentAction = 'meetingSchedules';
  protected action = 'meetingSchedules';

  constructor(
    protected http: HttpClient,
    protected authService: AuthService,
    protected contentService: MeetingContentService
  ) {
    super(http, authService);
  }

  getAgendas(timeId: number,
    page?,
    itemsPerPage?
  ): Observable<PaginatedResult<MeetingAgenda[]>> {
    this.authService.renewToken();
    return this.getItemsFromUrl<MeetingAgenda>(
      `${this.baseUrl}meetingSchedules/${timeId}/agendas`,
      page,
      itemsPerPage
    );
  }
}
