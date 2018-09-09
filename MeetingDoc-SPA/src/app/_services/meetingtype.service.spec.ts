/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MeetingTypeService } from './meetingtype.service';

describe('Service: Meetingtype', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MeetingTypeService]
    });
  });

  it('should ...', inject([MeetingTypeService], (service: MeetingTypeService) => {
    expect(service).toBeTruthy();
  }));
});
