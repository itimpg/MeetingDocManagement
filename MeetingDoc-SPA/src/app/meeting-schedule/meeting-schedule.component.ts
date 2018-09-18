import { Component } from '@angular/core';
import { MeetingSchedule } from '../_models/meeting-schedule';
import { BaseListComponent } from '../_components/baselist.component';
import { MeetingScheduleService } from '../_services/meeting-schedule.service';
import { AlertifyService } from '../_services/alertify.service';
import { BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { ShowModalParam } from '../_models/ShowModalParam';

@Component({
  selector: 'app-meeting-schedule',
  templateUrl: './meeting-schedule.component.html',
  styleUrls: ['./meeting-schedule.component.css']
})
export class MeetingScheduleComponent extends BaseListComponent<
  MeetingSchedule
> {
  actionName = 'meetingschedule';
  titleName = 'Meeting Schedule';
  itemName = 'Meeting Schedule';

  constructor(
    protected service: MeetingScheduleService,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router
  ) {
    super(service, alertify, modalService, route, router);
  }

  viewSubItem(item: MeetingSchedule) {
    this.router.navigate([`meetingSchedule/${item.id}/meeting`]);
  }

  showModal(initialState: ShowModalParam): void {}
}
