import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { MeetingContent } from '../_models/MeetingContent';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { MoveContent } from '../_models/MoveContent';
import { Observable } from 'rxjs';
import { MeetingAgenda } from '../_models/MeetingAgenda';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class MeetingContentService extends BaseService<MeetingContent> {
  protected parentAction = 'meetingagendas';
  protected action = 'meetingcontents';

  constructor(protected http: HttpClient, protected authService: AuthService) {
    super(http, authService);
  }

  moveContent(model: MoveContent) {
    this.authService.renewToken();
    return this.http.post(`${this.baseUrl}${this.action}/movecontent`, model);
  }

  getAgendas(contentId: number): Observable<MeetingAgenda[]> {
    this.authService.renewToken();
    const url = `${this.baseUrl}${this.action}/${contentId}/agendas`;
    return this.http.get<MeetingAgenda[]>(url);
  }

  getContents(
    agendaId: number,
    page?,
    itemsPerPage?
  ): Observable<PaginatedResult<MeetingContent[]>> {
    this.authService.renewToken();
    return this.getItemsFromUrl<MeetingContent>(
      `${this.baseUrl}meetingagendas/${agendaId}/schedulecontents`,
      page,
      itemsPerPage
    );
  }
}
