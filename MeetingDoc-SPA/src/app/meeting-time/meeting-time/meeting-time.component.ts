import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../_components/base.component';
import { MeetingTime } from '../../_models/MeetingTime';
import { BsModalRef } from 'ngx-bootstrap';
import { AlertifyService } from '../../_services/alertify.service';
import { MeetingTimeService } from '../../_services/meetingtime.service';

@Component({
  selector: 'app-meeting-time',
  templateUrl: './meeting-time.component.html',
  styleUrls: ['./meeting-time.component.css']
})
export class MeetingTimeComponent extends BaseComponent<MeetingTime> {
  protected action = 'Meeting Time';

  constructor(
    public bsModalRef: BsModalRef,
    protected service: MeetingTimeService,
    protected alertify: AlertifyService
  ) {
    super(bsModalRef, service, alertify);
  }

  PrepareBeforeSave(): MeetingTime {
    this.model.meetingTopicId = this.parentId;
    return this.model;
  }
}
