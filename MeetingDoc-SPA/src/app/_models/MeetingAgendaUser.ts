import { BaseModel } from './BaseModel';

export class MeetingAgendaUser extends BaseModel {
  userId: number;
  userFullName: string;
  isSelected: number;
}
