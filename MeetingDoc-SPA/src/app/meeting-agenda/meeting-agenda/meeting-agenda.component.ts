import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../_components/base.component';
import { BsModalRef } from 'ngx-bootstrap';
import { MeetingAgendaService } from '../../_services/meeting-agenda.service';
import { AlertifyService } from '../../_services/alertify.service';
import { MeetingAgenda } from '../../_models/MeetingAgenda';

@Component({
  selector: 'app-meeting-agenda',
  templateUrl: './meeting-agenda.component.html',
  styleUrls: ['./meeting-agenda.component.css']
})
export class MeetingAgendaComponent extends BaseComponent<MeetingAgenda> {
  protected action = 'Meeting Content';

  constructor(
    public bsModalRef: BsModalRef,
    protected service: MeetingAgendaService,
    protected alertify: AlertifyService
  ) {
    super(bsModalRef, service, alertify);
  }

  PrepareBeforeSave(): MeetingAgenda {
    this.model.meetingTimeId = this.parentId;
    return this.model;
  }
}
