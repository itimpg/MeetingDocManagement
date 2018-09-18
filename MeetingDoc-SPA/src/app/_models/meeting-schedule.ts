import { BaseModel } from './BaseModel';

export class MeetingSchedule extends BaseModel {
  userId: number;
  meetingType: string;
  meetingTopic: string;
  meetingTimeCount: number;
  meetingFiscalYear: string;
  meetingDateTime: Date;
  meetingPlace: string;
}
