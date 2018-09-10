import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { MeetingTopic } from '../_models/MeetingTopic';
import { MeetingTopicService } from '../_services/meetingtopic.service';
import { BaseListByParentResolver } from './baseListByParent.resolver';

@Injectable()
export class MeetingTopicListResolver extends BaseListByParentResolver<
  MeetingTopic
> {
  constructor(
    protected service: MeetingTopicService,
    protected router: Router,
    protected alertify: AlertifyService
  ) {
    super(service, router, alertify);
  }
}
