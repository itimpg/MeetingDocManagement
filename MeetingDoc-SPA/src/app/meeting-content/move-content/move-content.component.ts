import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { MeetingContentService } from '../../_services/meeting-content.service';
import { AlertifyService } from '../../_services/alertify.service';
import { MeetingAgenda } from '../../_models/MeetingAgenda';
import { MoveContent } from '../../_models/MoveContent';

@Component({
  selector: 'app-move-content',
  templateUrl: './move-content.component.html',
  styleUrls: ['./move-content.component.css']
})
export class MoveContentComponent implements OnInit {
  contentId: number;
  agendas: MeetingAgenda[];
  model: any;

  constructor(
    public bsModalRef: BsModalRef,
    protected service: MeetingContentService,
    protected alertify: AlertifyService
  ) {
    this.model = {};
  }

  ngOnInit() {
    this.service.getAgendas(this.contentId).subscribe(
      result => {
        this.agendas = result;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  saveItem() {
    const moveContent = new MoveContent();
    moveContent.contentId = this.contentId;
    moveContent.agendaId = this.model.agendaId;
    this.service.moveContent(moveContent).subscribe(
      success => {
        this.alertify.message('บันทึกสำเร็จ');
        this.bsModalRef.hide();
      },
      error => {
        this.alertify.error(error);
      }
    );
  }
}
