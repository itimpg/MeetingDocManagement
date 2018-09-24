import { Injectable } from '@angular/core';
import { MeetingType } from '../_models/MeetingType';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class MeetingTypeService extends BaseService<MeetingType> {
  protected parentAction: '';
  protected action = 'meetingTypes';

  constructor(protected http: HttpClient, protected authService: AuthService) {
    super(http, authService);
  }

  getActives(): Observable<MeetingType[]> {
    this.authService.renewToken();
    return this.http.get<MeetingType[]>(
      `${this.baseUrl}${this.action}/actives`
    );
  }
}
