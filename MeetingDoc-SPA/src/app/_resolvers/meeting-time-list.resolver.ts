import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { BaseListByParentResolver } from './baseListByParent.resolver';
import { MeetingTime } from '../_models/MeetingTime';
import { MeetingTimeService } from '../_services/meetingtime.service';

@Injectable()
export class MeetingTimeListResolver extends BaseListByParentResolver<MeetingTime>
{
  constructor(
    protected service: MeetingTimeService,
    protected router: Router,
    protected alertify: AlertifyService
  ) {
    super(service, router, alertify);
  }
}
