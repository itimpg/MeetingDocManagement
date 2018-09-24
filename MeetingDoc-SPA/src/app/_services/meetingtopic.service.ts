import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { MeetingTopic } from '../_models/MeetingTopic';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MeetingTopicService extends BaseService<MeetingTopic> {
  protected parentAction = 'meetingTypes';
  protected action = 'meetingTopics';

  constructor(protected http: HttpClient, protected authService: AuthService) {
    super(http, authService);
  }

  getActives(typeId: number): Observable<MeetingTopic[]> {
    this.authService.renewToken();
    return this.http.get<MeetingTopic[]>(
      `${this.baseUrl}meetingtypes/${typeId}/activeTopics`
    );
  }
}
