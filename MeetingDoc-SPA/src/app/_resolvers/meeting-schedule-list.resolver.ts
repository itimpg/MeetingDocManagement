import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { MeetingSchedule } from '../_models/meeting-schedule';
import { MeetingScheduleService } from '../_services/meeting-schedule.service';
import { BaseListResolver } from './baseList.resolver';

@Injectable()
export class MeetingScheduleListResolver extends BaseListResolver<
  MeetingSchedule
> {
  constructor(
    protected service: MeetingScheduleService,
    protected router: Router,
    protected alertify: AlertifyService
  ) {
    super(service, router, alertify);
  }
}
