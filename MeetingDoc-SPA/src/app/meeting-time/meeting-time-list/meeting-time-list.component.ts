import { Component } from '@angular/core';
import { BaseListComponent } from '../../_components/baselist.component';
import { MeetingTime } from '../../_models/MeetingTime';
import { MeetingTimeComponent } from '../meeting-time/meeting-time.component';
import { AlertifyService } from '../../_services/alertify.service';
import { BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { MeetingTimeService } from '../../_services/meetingtime.service';
import { ShowModalParam } from '../../_models/ShowModalParam';

@Component({
  selector: 'app-meeting-time-list',
  templateUrl: './meeting-time-list.component.html',
  styleUrls: ['./meeting-time-list.component.css']
})
export class MeetingTimeListComponent extends BaseListComponent<MeetingTime> {
  actionName = 'meetingtime';
  titleName = 'วัน/เวลา การประชุม';
  itemName = 'วัน/เวลา การประชุม';

  showModal(initialState: ShowModalParam): void {
    this.bsModalRef = this.modalService.show(MeetingTimeComponent, {
      initialState
    });
  }

  constructor(
    protected service: MeetingTimeService,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router
  ) {
    super(service, alertify, modalService, route, router);
  }

  viewSubItem(item: MeetingTime) {
    this.router.navigate([`meetingtimes/${item.id}/agendas`]);
  }
}
