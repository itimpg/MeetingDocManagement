import { Component, OnInit } from '@angular/core';
import { BaseListComponent } from '../../_components/baselist.component';
import { MeetingContentComponent } from '../meeting-content/meeting-content.component';
import { MeetingContent } from '../../_models/MeetingContent';
import { AlertifyService } from '../../_services/alertify.service';
import { BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { MeetingContentService } from '../../_services/meeting-content.service';
import { ShowModalParam } from '../../_models/ShowModalParam';
import { MeetingTopicComponent } from '../../meeting-topic/meeting-topic/meeting-topic.component';
import { combineLatest } from 'rxjs';
import { MoveContentComponent } from '../move-content/move-content.component';

@Component({
  selector: 'app-meeting-content-list',
  templateUrl: './meeting-content-list.component.html',
  styleUrls: ['./meeting-content-list.component.css']
})
export class MeetingContentListComponent extends BaseListComponent<
  MeetingContent
> {
  actionName = 'meetingcontent';
  titleName = 'ข้อมูลและเอกสาร';
  itemName = 'ข้อมูลและเอกสาร';

  showModal(initialState: ShowModalParam): void {
    this.bsModalRef = this.modalService.show(MeetingContentComponent, {
      initialState
    });
  }

  constructor(
    protected service: MeetingContentService,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router
  ) {
    super(service, alertify, modalService, route, router);
  }

  viewSubItem(item: MeetingContent) {}

  moveItem(item: MeetingContent) {
    const combine = combineLatest(
      this.modalService.onShow,
      this.modalService.onShown,
      this.modalService.onHide,
      this.modalService.onHidden
    ).subscribe(() => {
      this.loadItems();
      this.unsubscribe();
    });

    this.subscriptions.push(combine);

    const initialState = { contentId: item.id };
    this.bsModalRef = this.modalService.show(MoveContentComponent, {
      initialState
    });
  }
}
