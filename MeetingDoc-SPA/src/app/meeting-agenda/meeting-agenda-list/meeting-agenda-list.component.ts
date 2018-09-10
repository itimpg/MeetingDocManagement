import { Component } from '@angular/core';
import { BaseListComponent } from '../../_components/baselist.component';
import { MeetingAgenda } from '../../_models/MeetingAgenda';
import { MeetingAgendaComponent } from '../meeting-agenda/meeting-agenda.component';
import { AlertifyService } from '../../_services/alertify.service';
import { BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { MeetingAgendaService } from '../../_services/meeting-agenda.service';

@Component({
  selector: 'app-meeting-agenda-list',
  templateUrl: './meeting-agenda-list.component.html',
  styleUrls: ['./meeting-agenda-list.component.css']
})
export class MeetingAgendaListComponent extends BaseListComponent<
  MeetingAgenda
> {
  actionName = 'meetingagenda';
  titleName = 'Meeting Agendas';
  itemName = 'Meeting Agenda';

  showModal(itemId: number, isEditable: boolean): void {
    this.bsModalRef = this.modalService.show(MeetingAgendaComponent);
    this.bsModalRef.content.setModel(itemId, isEditable, this.parentId);
  }

  constructor(
    protected service: MeetingAgendaService,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router
  ) {
    super(service, alertify, modalService, route, router);
  }

  viewSubItem(item: MeetingAgenda) {
    this.router.navigate([`meetingagendas/${item.id}/contents`]);
  }
}
