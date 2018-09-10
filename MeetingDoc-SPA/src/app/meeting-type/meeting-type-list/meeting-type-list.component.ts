import { Component } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap';
import { AlertifyService } from '../../_services/alertify.service';

import { ActivatedRoute, Router } from '@angular/router';
import { BaseListComponent } from '../../_components/baselist.component';
import { MeetingType } from '../../_models/MeetingType';
import { MeetingTypeService } from 'src/app/_services/meetingtype.service';
import { MeetingTypeComponent } from '../meeting-type/meeting-type.component';

@Component({
  selector: 'app-meeting-type-list',
  templateUrl: './meeting-type-list.component.html',
  styleUrls: ['./meeting-type-list.component.css']
})
export class MeetingTypeListComponent extends BaseListComponent<MeetingType> {
  actionName = 'meetingType';
  titleName = 'Meeting Types';
  itemName = 'Meeting Type';

  showModal(itemId: number, isEditable: boolean): void {
    this.bsModalRef = this.modalService.show(MeetingTypeComponent);
    this.bsModalRef.content.setModel(itemId, isEditable);
  }

  constructor(
    protected service: MeetingTypeService,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router
  ) {
    super(service, alertify, modalService, route, router);
  }

  viewSubItem(item: MeetingType) {
    this.router.navigate([`meetingTypes/${item.id}/topics`]);
  }
}
