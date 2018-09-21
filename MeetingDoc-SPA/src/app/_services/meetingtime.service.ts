import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { MeetingTime } from '../_models/MeetingTime';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class MeetingTimeService extends BaseService<MeetingTime> {
  protected parentAction = 'meetingtopics';
  protected action = 'meetingtimes';

  constructor(protected http: HttpClient, protected authService: AuthService) {
    super(http, authService);
  }
}
