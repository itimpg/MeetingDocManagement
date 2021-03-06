import { Routes } from '@angular/router';
import { UserListComponent } from './users/user-list/user-list.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_guards/auth.guard';
import { AdminGuard } from './_guards/admin.guard';
import { WriterGuard } from './_guards/writer.guard';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { UserListResolver } from './_resolvers/userlist.resolver';
import { MeetingTypeListComponent } from './meeting-type/meeting-type-list/meeting-type-list.component';
import { MeetingTypeListResolver } from './_resolvers/meetingtypelist.resolver';
import { MeetingTopicListComponent } from './meeting-topic/meeting-topic-list/meeting-topic-list.component';
import { MeetingTopicListResolver } from './_resolvers/meetingtopiclist.resolver';
import { MeetingTimeListResolver } from './_resolvers/meeting-time-list.resolver';
import { MeetingTimeListComponent } from './meeting-time/meeting-time-list/meeting-time-list.component';
import { MeetingAgendaListComponent } from './meeting-agenda/meeting-agenda-list/meeting-agenda-list.component';
import { MeetingAgendaListResolver } from './_resolvers/meeting-agenda-list.resolver';
import { MeetingContentListComponent } from './meeting-content/meeting-content-list/meeting-content-list.component';
import { MeetingContentListResolver } from './_resolvers/meeting-content-list.resolver';
import { MeetingScheduleComponent } from './meeting-schedule/meeting-schedule.component';
import { UserGuard } from './_guards/user.guard';
import { MeetingScheduleListResolver } from './_resolvers/meeting-schedule-list.resolver';
import { MeetingScheduleAgendaComponent } from './meeting-schedule-agenda/meeting-schedule-agenda.component';
import { MeetingScheduleAgendaListResolver } from './_resolvers/meeting-schedule-agenda-list.resolver';
import { MeetingReaderResolver } from './_resolvers/meeting-reader.resolver';
import { MeetingReaderComponent } from './meeting-reader/meeting-reader.component';
import { WhiteboardComponent } from './whiteboard/whiteboard.component';

export const appRoutes: Routes = [
  { path: 'test', component: WhiteboardComponent },
  { path: 'login', component: LoginComponent },
  { path: 'forgetpassword', component: ForgetPasswordComponent },
  {
    path: 'users',
    runGuardsAndResolvers: 'always',
    canActivate: [AdminGuard],
    component: UserListComponent,
    resolve: { user: UserListResolver }
  },
  {
    path: 'meetingtypes',
    runGuardsAndResolvers: 'always',
    canActivate: [WriterGuard],
    component: MeetingTypeListComponent,
    resolve: { meetingType: MeetingTypeListResolver }
  },
  {
    path: 'meetingTypes/:id/topics',
    runGuardsAndResolvers: 'always',
    canActivate: [WriterGuard],
    component: MeetingTopicListComponent,
    resolve: { meetingTopic: MeetingTopicListResolver }
  },
  {
    path: 'meetingtopics/:id/times',
    runGuardsAndResolvers: 'always',
    canActivate: [WriterGuard],
    component: MeetingTimeListComponent,
    resolve: { meetingtime: MeetingTimeListResolver }
  },
  {
    path: 'meetingtimes/:id/agendas',
    runGuardsAndResolvers: 'always',
    canActivate: [WriterGuard],
    component: MeetingAgendaListComponent,
    resolve: { meetingagenda: MeetingAgendaListResolver }
  },
  {
    path: 'meetingagendas/:id/contents',
    runGuardsAndResolvers: 'always',
    canActivate: [WriterGuard],
    component: MeetingContentListComponent,
    resolve: { meetingcontent: MeetingContentListResolver }
  },
  {
    path: 'home',
    runGuardsAndResolvers: 'always',
    canActivate: [UserGuard],
    component: MeetingScheduleComponent,
    resolve: { meetingschedule: MeetingScheduleListResolver }
  },
  {
    path: 'meetingSchedule/:id/agendas',
    runGuardsAndResolvers: 'always',
    canActivate: [UserGuard],
    component: MeetingScheduleAgendaComponent,
    resolve: { meetingagenda: MeetingScheduleAgendaListResolver }
  },
  {
    path: 'meetingSchedule/:id/agendas/:agendaId/read',
    runGuardsAndResolvers: 'always',
    canActivate: [UserGuard],
    component: MeetingReaderComponent,
    resolve: { meetingContent: MeetingReaderResolver }
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];
