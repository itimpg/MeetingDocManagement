import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { MeetingTime } from '../_models/MeetingTime';

@Injectable({
  providedIn: 'root'
})
export class MeetingTimeService extends BaseService<MeetingTime> {
  protected parentAction = 'meetingtopics';
  protected action = 'meetingtimes';

  constructor(protected http: HttpClient) {
    super(http);
  }
}
