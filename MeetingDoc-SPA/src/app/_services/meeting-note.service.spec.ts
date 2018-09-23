/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MeetingNoteService } from './meeting-note.service';

describe('Service: MeetingNote', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MeetingNoteService]
    });
  });

  it('should ...', inject([MeetingNoteService], (service: MeetingNoteService) => {
    expect(service).toBeTruthy();
  }));
});
