import { Component, ElementRef, ViewChild } from '@angular/core';
import { BaseComponent } from '../../_components/base.component';
import { MeetingContent } from '../../_models/MeetingContent';
import { BsModalRef } from 'ngx-bootstrap';
import { AlertifyService } from '../../_services/alertify.service';
import { MeetingContentService } from '../../_services/meeting-content.service';

@Component({
  selector: 'app-meeting-content',
  templateUrl: './meeting-content.component.html',
  styleUrls: ['./meeting-content.component.css']
})
export class MeetingContentComponent extends BaseComponent<MeetingContent> {
  @ViewChild('myImage')
  myImage: ElementRef;

  protected action = 'ข้อมูลและเอกสาร';

  constructor(
    public bsModalRef: BsModalRef,
    protected service: MeetingContentService,
    protected alertify: AlertifyService
  ) {
    super(bsModalRef, service, alertify);
  }

  initAdd() {
    this.model.ordinal = this.total + 1;
  }

  PrepareBeforeSave(): MeetingContent {
    this.model.meetingAgendaId = this.parentId;
    const width = this.myImage.nativeElement.offsetWidth;
    const height = this.myImage.nativeElement.offsetHeight;
    if (width) {
      this.model.ratio = height / width;
    } else {
      this.model.ratio = 1;
    }
    return this.model;
  }

  readUrl(event: any) {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();

      reader.onload = (e: ProgressEvent) => {
        if (e.loaded > 2.5 * 1028 * 1028) {
          this.alertify.error('กรุณาเลือกไฟล์ขนาดไม่เกิน 2.5 Mb');
        } else {
          this.model.fileBase64 = (<FileReader>e.target).result.toString();
        }
      };

      reader.readAsDataURL(event.target.files[0]);
    }
  }
}
