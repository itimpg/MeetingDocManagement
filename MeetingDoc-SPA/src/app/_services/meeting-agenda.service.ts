import { Injectable } from '@angular/core';
import { MeetingAgenda } from '../_models/MeetingAgenda';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MeetingAgendaService extends BaseService<MeetingAgenda> {
  protected parentAction = 'meetingtimes';
  protected action = 'meetingagendas';

  constructor(protected http: HttpClient) {
    super(http);
  }
}
