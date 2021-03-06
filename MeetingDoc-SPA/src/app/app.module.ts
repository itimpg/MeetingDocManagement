import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';

import {
  BsDropdownModule,
  BsModalService,
  ModalModule,
  BsDatepickerModule,
  BsLocaleService,
  TimepickerModule,
  CollapseModule
} from 'ngx-bootstrap';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BreadcrumbsModule, BreadcrumbsService } from 'ng6-breadcrumbs';

import { environment } from '../environments/environment';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { UsersService } from './_services/users.service';
import { AuthService } from './_services/auth.service';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { LoginComponent } from './login/login.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { UserComponent } from './users/user/user.component';
import { AlertifyService } from './_services/alertify.service';
import { UserDetailResolver } from './_resolvers/user.resolver';
import { UserListResolver } from './_resolvers/userlist.resolver';
import { MeetingTypeListComponent } from './meeting-type/meeting-type-list/meeting-type-list.component';
import { MeetingTypeComponent } from './meeting-type/meeting-type/meeting-type.component';
import { MeetingTypeListResolver } from './_resolvers/meetingtypelist.resolver';
import { MeetingTypeDetailResolver } from './_resolvers/meetingtype.resolver';
import { MeetingTypeService } from './_services/meetingtype.service';
import { MeetingTopicListComponent } from './meeting-topic/meeting-topic-list/meeting-topic-list.component';
import { MeetingTopicComponent } from './meeting-topic/meeting-topic/meeting-topic.component';
import { MeetingTopicListResolver } from './_resolvers/meetingtopiclist.resolver';
import { MeetingTopicService } from './_services/meetingtopic.service';
import { MeetingTimeComponent } from './meeting-time/meeting-time/meeting-time.component';
import { MeetingTimeService } from './_services/meetingtime.service';
import { MeetingTimeListComponent } from './meeting-time/meeting-time-list/meeting-time-list.component';
import { MeetingTimeListResolver } from './_resolvers/meeting-time-list.resolver';
import { MeetingAgendaListComponent } from './meeting-agenda/meeting-agenda-list/meeting-agenda-list.component';
import { MeetingAgendaComponent } from './meeting-agenda/meeting-agenda/meeting-agenda.component';
import { MeetingContentListComponent } from './meeting-content/meeting-content-list/meeting-content-list.component';
import { MeetingContentComponent } from './meeting-content/meeting-content/meeting-content.component';
import { MeetingAgendaService } from './_services/meeting-agenda.service';
import { MeetingContentService } from './_services/meeting-content.service';
import { MeetingAgendaListResolver } from './_resolvers/meeting-agenda-list.resolver';
import { MeetingContentListResolver } from './_resolvers/meeting-content-list.resolver';
import { NumberOnlyDirective } from './_directives/number-only.directive';
import { ThaiNumberPipe } from './_pipes/thai-number.pipe';
import { ThaiYearPipe } from './_pipes/thai-year.pipe';
import { ThaiDatePipe } from './_pipes/thai-date.pipe';

import { defineLocale } from 'ngx-bootstrap/chronos';
import { thLocale } from 'ngx-bootstrap/locale';
import { AdminGuard } from './_guards/admin.guard';
import { WriterGuard } from './_guards/writer.guard';
import { MeetingScheduleComponent } from './meeting-schedule/meeting-schedule.component';
import { UserGuard } from './_guards/user.guard';
import { MeetingScheduleService } from './_services/meeting-schedule.service';
import { UnAuthGuard } from './_guards/unauth.guard';
import { MoveContentComponent } from './meeting-content/move-content/move-content.component';
import { MeetingScheduleAgendaComponent } from './meeting-schedule-agenda/meeting-schedule-agenda.component';
import { MeetingScheduleAgendaListResolver } from './_resolvers/meeting-schedule-agenda-list.resolver';
import { MeetingScheduleListResolver } from './_resolvers/meeting-schedule-list.resolver';
import { MeetingReaderComponent } from './meeting-reader/meeting-reader.component';
import { MeetingReaderResolver } from './_resolvers/meeting-reader.resolver';
import { CanvasWhiteboardModule } from 'node_modules/ng2-canvas-whiteboard';
import { WhiteboardComponent } from './whiteboard/whiteboard.component';
import { MeetingNoteService } from './_services/meeting-note.service';

defineLocale('th', thLocale);

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      ChangePasswordComponent,
      LoginComponent,
      ForgetPasswordComponent,
      UserListComponent,
      UserComponent,
      MeetingTypeListComponent,
      MeetingTypeComponent,
      MeetingTopicListComponent,
      MeetingTopicComponent,
      MeetingTimeListComponent,
      MeetingTimeComponent,
      MeetingAgendaListComponent,
      MeetingAgendaComponent,
      MeetingContentListComponent,
      MeetingContentComponent,
      ThaiNumberPipe,
      ThaiYearPipe,
      ThaiDatePipe,
      NumberOnlyDirective,
      MeetingScheduleComponent,
      MoveContentComponent,
      MeetingScheduleAgendaComponent,
      MeetingReaderComponent,
      WhiteboardComponent
   ],
   imports: [
      CanvasWhiteboardModule,
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BreadcrumbsModule,
      ModalModule.forRoot(),
      BsDropdownModule.forRoot(),
      PaginationModule.forRoot(),
      BsDatepickerModule.forRoot(),
      TimepickerModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      CollapseModule.forRoot(),
      JwtModule.forRoot({
        config: {
          tokenGetter: tokenGetter,
          whitelistedDomains: [environment.jwtWhiteList],
          blacklistedRoutes: [environment.jwtWhiteList + '/api/auth']
        }
      })
  ],
  providers: [
    AlertifyService,
    ErrorInterceptorProvider,
    BsModalService,
    BsLocaleService,
    BreadcrumbsService,
    AuthGuard,
    UnAuthGuard,
    UserGuard,
    WriterGuard,
    AdminGuard,
    UserDetailResolver,
    UserListResolver,
    MeetingTypeListResolver,
    MeetingTypeDetailResolver,
    MeetingTopicListResolver,
    MeetingTimeListResolver,
    MeetingAgendaListResolver,
    MeetingContentListResolver,
    MeetingScheduleListResolver,
    MeetingScheduleAgendaListResolver,
    MeetingReaderResolver,
    AuthService,
    UsersService,
    MeetingTypeService,
    MeetingTopicService,
    MeetingTimeService,
    MeetingAgendaService,
    MeetingContentService,
    MeetingScheduleService,
    MeetingNoteService,
    ThaiNumberPipe,
    ThaiYearPipe,
    ThaiDatePipe
  ],
  entryComponents: [
    ChangePasswordComponent,
    UserComponent,
    MeetingTypeComponent,
    MeetingTopicComponent,
    MeetingTimeComponent,
    MeetingAgendaComponent,
    MeetingContentComponent,
    MoveContentComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
