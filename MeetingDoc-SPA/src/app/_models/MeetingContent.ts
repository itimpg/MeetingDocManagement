import { BaseModel } from './BaseModel';

export class MeetingContent extends BaseModel {
  fileName: string;
  ordinal: number;
  meetingAgendaId: number;
}
