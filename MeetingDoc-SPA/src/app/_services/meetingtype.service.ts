import { Injectable } from '@angular/core';
import { MeetingType } from '../_models/MeetingType';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class MeetingTypeService extends BaseService<MeetingType> {
  protected action = 'meetingTypes';
}
