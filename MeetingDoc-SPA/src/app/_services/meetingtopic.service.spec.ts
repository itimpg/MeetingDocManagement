/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MeetingtopicService } from './meetingtopic.service';

describe('Service: Meetingtopic', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MeetingtopicService]
    });
  });

  it('should ...', inject([MeetingtopicService], (service: MeetingtopicService) => {
    expect(service).toBeTruthy();
  }));
});
