import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService } from './base.service';
import { MeetingSchedule } from '../_models/meeting-schedule';
import { AuthService } from './auth.service';
import { MeetingContentService } from './meeting-content.service';
import { MeetingAgenda } from '../_models/MeetingAgenda';
import { PaginatedResult } from '../_models/pagination';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

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

  getAgendas(
    timeId: number,
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

  sendEmail(contentId: number, email: string) {
    this.authService.renewToken();
    return this.http.post(`${this.baseUrl}${this.action}/sharecontent`, {
      contentId: contentId,
      email: email
    });
  }

  getItemsByCriteria(
    typeId,
    topicId,
    page?,
    itemsPerPage?
  ): Observable<PaginatedResult<MeetingSchedule[]>> {
    this.authService.renewToken();

    const url = `${this.baseUrl}${this.action}`;
    const paginatedResult = new PaginatedResult<MeetingSchedule[]>();
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    params = params.append('meetingTypeId', typeId);
    params = params.append('meetingTopicId', topicId);

    return this.http.get<any>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(
            response.headers.get('Pagination')
          );
        }
        return paginatedResult;
      })
    );
  }
}
