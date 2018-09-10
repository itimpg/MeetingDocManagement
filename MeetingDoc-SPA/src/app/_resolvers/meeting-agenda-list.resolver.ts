import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { BaseListByParentResolver } from './baseListByParent.resolver';
import { MeetingAgenda } from '../_models/MeetingAgenda';
import { MeetingAgendaService } from '../_services/meeting-agenda.service';

@Injectable()
export class MeetingAgendaListResolver extends BaseListByParentResolver<
  MeetingAgenda
> {
  constructor(
    protected service: MeetingAgendaService,
    protected router: Router,
    protected alertify: AlertifyService
  ) {
    super(service, router, alertify);
  }
}
