import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';

import { PaginationModule } from 'ngx-bootstrap/pagination';

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
import { BsDropdownModule, BsModalService, ModalModule } from 'ngx-bootstrap';
import { MeetingComponent } from './meetings/meeting/meeting.component';
import { MeetingListComponent } from './meetings/meeting-list/meeting-list.component';
import { UserDetailResolver } from './_resolvers/user.resolver';
import { UserListResolver } from './_resolvers/userlist.resolver';
import { MeetingTypeListComponent } from './meeting-type/meeting-type-list/meeting-type-list.component';
import { MeetingTypeComponent } from './meeting-type/meeting-type/meeting-type.component';
import { MeetingTypeListResolver } from './_resolvers/meetingtypelist.resolver';
import { MeetingTypeDetailResolver } from './_resolvers/meetingtype.resolver';
import { MeetingTypeService } from './_services/meetingtype.service';

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

    MeetingListComponent,
    MeetingComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ModalModule.forRoot(),
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: [environment.jwtWhiteList],
        blacklistedRoutes: [environment.jwtWhiteList + '/api/auth']
      }
    })
  ],
  providers: [
    AuthService,
    UsersService,
    MeetingTypeService,
    AlertifyService,
    ErrorInterceptorProvider,
    BsModalService,
    AuthGuard,
    UserDetailResolver,
    UserListResolver,
    MeetingTypeListResolver,
    MeetingTypeDetailResolver
  ],
  entryComponents: [ChangePasswordComponent, UserComponent],
  bootstrap: [AppComponent]
})
export class AppModule {}
