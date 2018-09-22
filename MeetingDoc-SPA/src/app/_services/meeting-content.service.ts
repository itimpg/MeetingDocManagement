import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { MeetingContent } from '../_models/MeetingContent';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { MoveContent } from '../_models/MoveContent';

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
}
