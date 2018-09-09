import { BaseResolver } from './base.resolver';
import { MeetingType } from '../_models/MeetingType';
import { MeetingTypeService } from '../_services/meetingtype.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Injectable } from '@angular/core';

@Injectable()
export class MeetingTypeDetailResolver extends BaseResolver<MeetingType> {
  protected action = 'meetingtypes';

  constructor(
    protected service: MeetingTypeService,
    protected router: Router,
    protected alertify: AlertifyService
  ) {
    super(service, router, alertify);
  }
}
