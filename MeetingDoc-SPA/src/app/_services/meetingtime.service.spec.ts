/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MeetingtimeService } from './meetingtime.service';

describe('Service: Meetingtime', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MeetingtimeService]
    });
  });

  it('should ...', inject([MeetingtimeService], (service: MeetingtimeService) => {
    expect(service).toBeTruthy();
  }));
});
