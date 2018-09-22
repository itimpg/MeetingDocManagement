import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { MeetingContentService } from '../../_services/meeting-content.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-move-content',
  templateUrl: './move-content.component.html',
  styleUrls: ['./move-content.component.css']
})
export class MoveContentComponent implements OnInit {
  contentId: number;

  constructor(
    public bsModalRef: BsModalRef,
    protected service: MeetingContentService,
    protected alertify: AlertifyService
  ) {}

  ngOnInit() {}

  saveItem() {}
}
