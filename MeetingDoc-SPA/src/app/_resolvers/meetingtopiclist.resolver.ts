import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { BaseListResolver } from './baseList.resolver';
import { MeetingTopic } from '../_models/MeetingTopic';
import { MeetingTopicService } from '../_services/meetingtopic.service';

@Injectable()
export class MeetingTopicListResolver extends BaseListResolver<MeetingTopic> {
  constructor(
    protected service: MeetingTopicService,
    protected router: Router,
    protected alertify: AlertifyService
  ) {
    super(service, router, alertify);
  }
}
