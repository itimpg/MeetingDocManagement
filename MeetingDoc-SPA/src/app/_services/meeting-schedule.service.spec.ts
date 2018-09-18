/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MeetingScheduleService } from './meeting-schedule.service';

describe('Service: MeetingSchedule', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MeetingScheduleService]
    });
  });

  it('should ...', inject([MeetingScheduleService], (service: MeetingScheduleService) => {
    expect(service).toBeTruthy();
  }));
});
