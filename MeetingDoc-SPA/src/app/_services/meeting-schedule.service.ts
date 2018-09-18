import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { MeetingSchedule } from '../_models/meeting-schedule';

@Injectable({
  providedIn: 'root'
})
export class MeetingScheduleService extends BaseService<MeetingSchedule> {
  protected parentAction = 'meetingSchedules';
  protected action = 'meetingSchedules';

  constructor(protected http: HttpClient) {
    super(http);
  }
}
