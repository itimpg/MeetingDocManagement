import { Component } from '@angular/core';
import { MeetingAgenda } from '../_models/MeetingAgenda';
import { BaseListComponent } from '../_components/baselist.component';
import { MeetingAgendaService } from '../_services/meeting-agenda.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap';
import { ShowModalParam } from '../_models/ShowModalParam';
import { PaginatedResult } from '../_models/pagination';
import { MeetingScheduleService } from '../_services/meeting-schedule.service';
import { MeetingTopicService } from '../_services/meetingtopic.service';
import { MeetingTimeService } from '../_services/meetingtime.service';

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
  title = '';

  showModal(initialState: ShowModalParam): void {}

  constructor(
    protected service: MeetingAgendaService,
    protected scheduleService: MeetingScheduleService,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router,
    protected timeService: MeetingTimeService
  ) {
    super(service, alertify, modalService, route, router);
  }

  InitComponent() {
    this.timeService.getItem(this.parentId).subscribe(
      result => {
        this.title = result.meetingTopicName;
      },
      error => this.alertify.error(error)
    );
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
