import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { BaseListByParentResolver } from './baseListByParent.resolver';
import { MeetingContent } from '../_models/MeetingContent';
import { MeetingContentService } from '../_services/meeting-content.service';

@Injectable()
export class MeetingContentListResolver extends BaseListByParentResolver<
  MeetingContent
> {
  constructor(
    protected service: MeetingContentService,
    protected router: Router,
    protected alertify: AlertifyService
  ) {
    super(service, router, alertify);
  }
}
