import { Component, OnInit } from '@angular/core';
import { BaseListComponent } from '../../_components/baselist.component';
import { MeetingContentComponent } from '../meeting-content/meeting-content.component';
import { MeetingContent } from '../../_models/MeetingContent';
import { AlertifyService } from '../../_services/alertify.service';
import { BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { MeetingContentService } from '../../_services/meeting-content.service';

@Component({
  selector: 'app-meeting-content-list',
  templateUrl: './meeting-content-list.component.html',
  styleUrls: ['./meeting-content-list.component.css']
})
export class MeetingContentListComponent extends BaseListComponent<
  MeetingContent
> {
  actionName = 'meetingcontent';
  titleName = 'Meeting Contents';
  itemName = 'Meeting Content';

  showModal(itemId: number, isEditable: boolean): void {
    this.bsModalRef = this.modalService.show(MeetingContentComponent);
    this.bsModalRef.content.setModel(itemId, isEditable, this.parentId);
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
}
