import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { MeetingTopic } from '../_models/MeetingTopic';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MeetingTopicService extends BaseService<MeetingTopic> {
  protected parentAction = 'meetingTypes';
  protected action = 'meetingTopics';

  constructor(protected http: HttpClient) {
    super(http);
  }
}
