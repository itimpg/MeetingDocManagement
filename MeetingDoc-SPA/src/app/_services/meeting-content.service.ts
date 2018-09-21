import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { MeetingContent } from '../_models/MeetingContent';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class MeetingContentService extends BaseService<MeetingContent> {
  protected parentAction = 'meetingagendas';
  protected action = 'meetingcontents';

  constructor(protected http: HttpClient, protected authService: AuthService) {
    super(http, authService);
  }
}
