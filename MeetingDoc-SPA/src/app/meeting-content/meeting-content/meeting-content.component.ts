import { Component } from '@angular/core';
import { BaseComponent } from '../../_components/base.component';
import { MeetingContent } from '../../_models/MeetingContent';
import { BsModalRef } from 'ngx-bootstrap';
import { AlertifyService } from '../../_services/alertify.service';
import { MeetingContentService } from '../../_services/meeting-content.service';

@Component({
  selector: 'app-meeting-content',
  templateUrl: './meeting-content.component.html',
  styleUrls: ['./meeting-content.component.css']
})
export class MeetingContentComponent extends BaseComponent<MeetingContent> {
  protected action = 'Meeting Content';

  constructor(
    public bsModalRef: BsModalRef,
    protected service: MeetingContentService,
    protected alertify: AlertifyService
  ) {
    super(bsModalRef, service, alertify);
  }

  PrepareBeforeSave(): MeetingContent {
    this.model.meetingAgendaId = this.parentId;
    return this.model;
  }
}
