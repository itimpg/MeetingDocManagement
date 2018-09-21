import { Component } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap';
import { AlertifyService } from '../../_services/alertify.service';

import { ActivatedRoute, Router } from '@angular/router';
import { BaseListComponent } from '../../_components/baselist.component';
import { MeetingType } from '../../_models/MeetingType';
import { MeetingTypeService } from 'src/app/_services/meetingtype.service';
import { MeetingTypeComponent } from '../meeting-type/meeting-type.component';
import { ShowModalParam } from '../../_models/ShowModalParam';
import { BreadcrumbsService } from 'ng6-breadcrumbs';

@Component({
  selector: 'app-meeting-type-list',
  templateUrl: './meeting-type-list.component.html',
  styleUrls: ['./meeting-type-list.component.css']
})
export class MeetingTypeListComponent extends BaseListComponent<MeetingType> {
  actionName = 'meetingType';
  titleName = 'ประเภท/กลุ่ม การประชุม';
  itemName = 'ประเภท/กลุ่ม การประชุม';

  showModal(initialState: ShowModalParam): void {
    this.bsModalRef = this.modalService.show(MeetingTypeComponent, {
      initialState
    });
  }

  constructor(
    protected service: MeetingTypeService,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router,
    private breadcrumbs: BreadcrumbsService
  ) {
    super(service, alertify, modalService, route, router);
  }

  viewSubItem(item: MeetingType) {
    this.router.navigate([`meetingTypes/${item.id}/topics`]);
  }
}
