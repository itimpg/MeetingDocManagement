import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { AlertifyService } from '../../_services/alertify.service';
import { BaseComponent } from '../../_components/base.component';
import { MeetingTopic } from '../../_models/MeetingTopic';
import { MeetingTopicService } from '../../_services/meetingtopic.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-meeting-topic',
  templateUrl: './meeting-topic.component.html',
  styleUrls: ['./meeting-topic.component.css']
})
export class MeetingTopicComponent extends BaseComponent<MeetingTopic> {
  protected action = 'Meeting Topic';

  constructor(
    public bsModalRef: BsModalRef,
    protected service: MeetingTopicService,
    protected alertify: AlertifyService,
    protected router: Router
  ) {
    super(bsModalRef, service, alertify);
  }

  PrepareBeforeSave(): MeetingTopic {
    this.model.meetingTypeId = this.parentId;
    return this.model;
  }
}
