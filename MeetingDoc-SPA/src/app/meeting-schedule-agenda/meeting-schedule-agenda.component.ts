import { Component, OnInit } from '@angular/core';
import { MeetingAgenda } from '../_models/MeetingAgenda';
import { BaseListComponent } from '../_components/baselist.component';
import { MeetingAgendaService } from '../_services/meeting-agenda.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap';
import { ShowModalParam } from '../_models/ShowModalParam';
import { PaginatedResult } from '../_models/pagination';
import { MeetingScheduleService } from '../_services/meeting-schedule.service';

@Component({
  selector: 'app-meeting-schedule-agenda',
  templateUrl: './meeting-schedule-agenda.component.html',
  styleUrls: ['./meeting-schedule-agenda.component.css']
})
export class MeetingScheduleAgendaComponent extends BaseListComponent<
  MeetingAgenda
> {
  actionName = 'meetingagenda';
  titleName = 'ระเบียบวาระ การประชุม';
  itemName = 'ระเบียบวาระ การประชุม';

  showModal(initialState: ShowModalParam): void {}

  constructor(
    protected service: MeetingAgendaService,
    protected scheduleService: MeetingScheduleService,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router
  ) {
    super(service, alertify, modalService, route, router);
  }

  viewSubItem(item: MeetingAgenda) {
    this.router.navigate([
      `meetingSchedule/${this.parentId}/agendas/${item.id}/read`
    ]);
  }

  loadItems() {
    const observableCollection = this.scheduleService.getAgendas(
      this.parentId,
      this.pagination.currentPage,
      this.pagination.itemsPerPage
    );

    observableCollection.subscribe(
      (res: PaginatedResult<MeetingAgenda[]>) => {
        this.items = res.result;
        this.pagination = res.pagination;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }
}
