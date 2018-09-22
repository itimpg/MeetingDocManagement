import { BaseModel } from './BaseModel';

export class MeetingTime extends BaseModel {
  meetingTopicId: number;
  count: number;
  fiscalYear: string;
  meetingDate: Date;
  location: string;
  isDraft: boolean;
}
