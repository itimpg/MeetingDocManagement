import { Component } from '@angular/core';
import { MeetingAgenda } from '../_models/MeetingAgenda';
import { MeetingScheduleService } from '../_services/meeting-schedule.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { MeetingContentService } from '../_services/meeting-content.service';
import { MeetingContent } from '../_models/MeetingContent';
import { ShowModalParam } from '../_models/ShowModalParam';
import { BaseListComponent } from '../_components/baselist.component';
import { BsModalService } from 'ngx-bootstrap';
import { PaginatedResult } from '../_models/pagination';

@Component({
  selector: 'app-meeting-reader',
  templateUrl: './meeting-reader.component.html',
  styleUrls: ['./meeting-reader.component.css']
})
export class MeetingReaderComponent extends BaseListComponent<MeetingContent> {
  actionName = 'meetingContent';
  titleName = '';
  itemName = '';

  agendas: MeetingAgenda[];
  selectedAgenda: number;

  showModal(initialState: ShowModalParam): void {}

  constructor(
    protected scheduleService: MeetingScheduleService,
    protected service: MeetingContentService,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router
  ) {
    super(service, alertify, modalService, route, router);
  }

  InitComponent() {
    this.route.params.subscribe(params => {
      this.selectedAgenda = params['agendaId'];

      this.scheduleService.getAgendas(this.parentId, 1).subscribe(
        result => {
          this.agendas = result.result;
        },
        error => {
          this.alertify.error(error);
        }
      );
    });
  }

  loadItems() {
    const observableCollection = this.service.getContents(
      this.selectedAgenda,
      this.pagination.currentPage,
      this.pagination.itemsPerPage
    );

    observableCollection.subscribe(
      (res: PaginatedResult<MeetingContent[]>) => {
        this.items = res.result;
        this.pagination = res.pagination;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  viewSubItem(item: MeetingContent) {}

  goBack() {}

  goNext() {}

  onAgendaChanged(agendaId: number) {
    this.router.navigate([
      `meetingSchedule/${this.parentId}/agendas/${agendaId}/read`
    ]);
  }
}