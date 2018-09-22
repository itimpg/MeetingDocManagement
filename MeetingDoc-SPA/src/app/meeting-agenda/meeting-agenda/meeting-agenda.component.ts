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
  protected action = 'ระเบียบวาระ การประชุม';

  constructor(
    public bsModalRef: BsModalRef,
    protected service: MeetingAgendaService,
    protected alertify: AlertifyService
  ) {
    super(bsModalRef, service, alertify);
  }

  InitComponent() {
    this.service.getItem(this.itemId).subscribe(
      result => {
        this.model = this.ConvertResultToModel(result);
        if (this.itemId === 0) {
          this.isEditable = true;
          this.title = `เพิ่ม ${this.action}`;
          this.model.isDraft = true;
          this.model.number = this.total + 1;
        } else {
          this.title = this.isEditable
            ? `แก้ไข  ${this.action}`
            : `ดูรายละเอียด ${this.action}`;
        }
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  PrepareBeforeSave(): MeetingAgenda {
    this.model.meetingTimeId = this.parentId;
    return this.model;
  }
}
