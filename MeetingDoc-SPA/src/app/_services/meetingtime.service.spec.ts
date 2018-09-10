/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MeetingTimeService } from './meetingtime.service';

describe('Service: Meetingtime', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MeetingTimeService]
    });
  });

  it('should ...', inject([MeetingTimeService], (service: MeetingTimeService) => {
    expect(service).toBeTruthy();
  }));
});
