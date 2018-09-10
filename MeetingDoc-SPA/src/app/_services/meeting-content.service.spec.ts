/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MeetingContentService } from './meeting-content.service';

describe('Service: MeetingContent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MeetingContentService]
    });
  });

  it('should ...', inject([MeetingContentService], (service: MeetingContentService) => {
    expect(service).toBeTruthy();
  }));
});
