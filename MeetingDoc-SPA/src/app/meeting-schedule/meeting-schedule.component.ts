import { Component } from '@angular/core';
import { MeetingSchedule } from '../_models/meeting-schedule';
import { BaseListComponent } from '../_components/baselist.component';
import { MeetingScheduleService } from '../_services/meeting-schedule.service';
import { AlertifyService } from '../_services/alertify.service';
import { BsModalService } from 'ngx-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { ShowModalParam } from '../_models/ShowModalParam';
import { MeetingType } from '../_models/MeetingType';
import { MeetingTopic } from '../_models/MeetingTopic';
import { MeetingTypeService } from '../_services/meetingtype.service';
import { MeetingTopicService } from '../_services/meetingtopic.service';

@Component({
  selector: 'app-meeting-schedule',
  templateUrl: './meeting-schedule.component.html',
  styleUrls: ['./meeting-schedule.component.css']
})
export class MeetingScheduleComponent extends BaseListComponent<
  MeetingSchedule
> {
  actionName = 'meetingschedule';
  titleName = 'ตารางรายการกิจกรรมการประชุม';
  itemName = 'ตารางรายการกิจกรรมการประชุม';

  meetingTypes: MeetingType[];
  meetingTopics: MeetingTopic[];
  typeId: number;

  constructor(
    protected service: MeetingScheduleService,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router,
    protected meetingTypeService: MeetingTypeService,
    protected meetingTopicService: MeetingTopicService
  ) {
    super(service, alertify, modalService, route, router);
  }

  viewSubItem(item: MeetingSchedule) {
    this.router.navigate([`meetingSchedule/${item.id}/agendas`]);
  }

  showModal(initialState: ShowModalParam): void {}

  InitComponent() {
    this.typeId = 0;
    this.meetingTypeService.getActives().subscribe(
      result => {
        this.meetingTypes = result;
        const defaultType = new MeetingType();
        defaultType.id = 0;
        defaultType.name = '-- ทั้งหมด --';
        this.meetingTypes.unshift(defaultType);
      },
      error => {
        this.alertify.error(error);
      }
    );

    this.initMeetingTypes(0);
  }

  initMeetingTypes(typeId: number) {
    this.meetingTopicService.getActives(typeId).subscribe(
      result => {
        this.meetingTopics = result;
        const defaultTopic = new MeetingTopic();
        defaultTopic.id = 0;
        defaultTopic.name = '-- ทั้งหมด --';
        this.meetingTopics.unshift(defaultTopic);
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  searchData(typeId, topicId) {
    this.service.getItemsByCriteria(typeId, topicId, 1, 5).subscribe(
      result => {
        this.pagination = result.pagination;
        this.items = result.result;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  onMeetingTypeChanged(typeId) {
    this.initMeetingTypes(typeId);
    this.searchData(typeId, 0);
  }

  onMeetingTopicChanged(topicId) {
    this.searchData(this.typeId, topicId);
  }
}
