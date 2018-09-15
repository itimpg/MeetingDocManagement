import { BaseModel } from './BaseModel';

export class MeetingContent extends BaseModel {
  fileName: string;
  fileBase64: string;
  ordinal: number;
  meetingAgendaId: number;
}
