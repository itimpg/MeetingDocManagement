import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { BaseListResolver } from './baseList.resolver';
import { MeetingType } from '../_models/MeetingType';
import { MeetingTypeService } from 'src/app/_services/meetingtype.service';

@Injectable()
export class MeetingTypeListResolver extends BaseListResolver<MeetingType> {
  constructor(
    protected service: MeetingTypeService,
    protected router: Router,
    protected alertify: AlertifyService
  ) {
    super(service, router, alertify);
  }
}
