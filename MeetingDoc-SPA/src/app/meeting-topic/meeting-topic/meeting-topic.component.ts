import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { AlertifyService } from '../../_services/alertify.service';
import { BaseComponent } from '../../_components/base.component';
import { MeetingTopic } from '../../_models/MeetingTopic';
import { MeetingTopicService } from '../../_services/meetingtopic.service';

@Component({
  selector: 'app-meeting-topic',
  templateUrl: './meeting-topic.component.html',
  styleUrls: ['./meeting-topic.component.css']
})
export class MeetingTopicComponent extends BaseComponent<MeetingTopic> {
  protected action = 'หัวข้อ/วาระ การประชุม';

  constructor(
    public bsModalRef: BsModalRef,
    protected service: MeetingTopicService,
    protected alertify: AlertifyService
  ) {
    super(bsModalRef, service, alertify);
  }

  PrepareBeforeSave(): MeetingTopic {
    this.model.meetingTypeId = this.parentId;
    return this.model;
  }

  initAdd() {
    this.model.isDraft = true;
  }
}
