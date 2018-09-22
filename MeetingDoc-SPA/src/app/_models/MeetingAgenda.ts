import { BaseModel } from './BaseModel';
import { MeetingAgendaUser } from './MeetingAgendaUser';

export class MeetingAgenda extends BaseModel {
  meetingTimeId: number;
  number: number;
  name: string;
  users: MeetingAgendaUser[];
  isDraft: boolean;
}
