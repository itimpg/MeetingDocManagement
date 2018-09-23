import { Injectable } from '@angular/core';
import { MeetingNote } from '../_models/meeting-note';
import { BaseService } from './base.service';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MeetingNoteService extends BaseService<MeetingNote> {
  protected parentAction = 'meetingcontents';
  protected action = 'meetingnotes';

  constructor(protected http: HttpClient, protected authService: AuthService) {
    super(http, authService);
  }
}
