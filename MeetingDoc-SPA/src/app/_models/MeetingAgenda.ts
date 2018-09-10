import { BaseModel } from './BaseModel';

export class MeetingAgenda extends BaseModel {
  meetingTimeId: number;
  number: number;
  name: string;
}
