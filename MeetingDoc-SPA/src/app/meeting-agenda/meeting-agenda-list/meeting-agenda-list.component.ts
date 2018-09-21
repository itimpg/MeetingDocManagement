import { Component } from '@angular/core';
import { BaseListComponent } from '../../_components/baselist.component';
import { MeetingAgenda } from '../../_models/MeetingAgenda';
import { MeetingAgendaComponent } from '../meeting-agenda/meeting-agenda.component';
import { AlertifyService } from '../../_services/alertify.service';
import { BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { MeetingAgendaService } from '../../_services/meeting-agenda.service';
import { ShowModalParam } from '../../_models/ShowModalParam';

@Component({
  selector: 'app-meeting-agenda-list',
  templateUrl: './meeting-agenda-list.component.html',
  styleUrls: ['./meeting-agenda-list.component.css']
})
export class MeetingAgendaListComponent extends BaseListComponent<
  MeetingAgenda
> {
  actionName = 'meetingagenda';
  titleName = 'ระเบียบวาระ การประชุม';
  itemName = 'ระเบียบวาระ การประชุม';

  showModal(initialState: ShowModalParam): void {
    this.bsModalRef = this.modalService.show(MeetingAgendaComponent, {
      initialState
    });
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
