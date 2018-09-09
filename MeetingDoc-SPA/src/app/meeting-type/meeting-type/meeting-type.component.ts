import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../_components/base.component';
import { MeetingType } from '../../_models/MeetingType';
import { MeetingTypeService } from '../../_services/meetingtype.service';
import { AlertifyService } from '../../_services/alertify.service';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-meeting-type',
  templateUrl: './meeting-type.component.html',
  styleUrls: ['./meeting-type.component.css']
})
export class MeetingTypeComponent extends BaseComponent<MeetingType> {
  protected action = 'Meeting Type';

  constructor(
    public bsModalRef: BsModalRef,
    protected service: MeetingTypeService,
    protected alertify: AlertifyService
  ) {
    super(bsModalRef, service, alertify);
  }
}
