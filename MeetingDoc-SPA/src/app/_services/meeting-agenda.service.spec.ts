/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MeetingAgendaService } from './meeting-agenda.service';

describe('Service: MeetingAgenda', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MeetingAgendaService]
    });
  });

  it('should ...', inject([MeetingAgendaService], (service: MeetingAgendaService) => {
    expect(service).toBeTruthy();
  }));
});
