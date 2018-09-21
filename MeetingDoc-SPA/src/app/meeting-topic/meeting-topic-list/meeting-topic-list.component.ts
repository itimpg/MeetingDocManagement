import { Component } from '@angular/core';
import { BaseListComponent } from '../../_components/baselist.component';
import { MeetingTopic } from '../../_models/MeetingTopic';
import { MeetingTopicComponent } from '../meeting-topic/meeting-topic.component';
import { MeetingTopicService } from '../../_services/meetingtopic.service';
import { AlertifyService } from '../../_services/alertify.service';
import { BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { ShowModalParam } from '../../_models/ShowModalParam';

@Component({
  selector: 'app-meeting-topic-list',
  templateUrl: './meeting-topic-list.component.html',
  styleUrls: ['./meeting-topic-list.component.css']
})
export class MeetingTopicListComponent extends BaseListComponent<MeetingTopic> {
  actionName = 'meetingTopic';
  titleName = 'หัวข้อ/วาระ การประชุม';
  itemName = 'หัวข้อ/วาระ การประชุม';

  showModal(initialState: ShowModalParam): void {
    this.bsModalRef = this.modalService.show(MeetingTopicComponent, {
      initialState
    });
  }

  constructor(
    protected service: MeetingTopicService,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router
  ) {
    super(service, alertify, modalService, route, router);
  }

  viewSubItem(item: MeetingTopic) {
    this.router.navigate([`meetingtopics/${item.id}/times`]);
  }
}
