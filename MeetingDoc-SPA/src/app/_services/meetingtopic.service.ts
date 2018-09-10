import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { MeetingTopic } from '../_models/MeetingTopic';

@Injectable({
  providedIn: 'root'
})
export class MeetingTopicService extends BaseService<MeetingTopic> {
  protected action = 'meetingTopics';
}
