import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { PaginationModule } from 'ngx-bootstrap/pagination';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { LoginComponent } from './login/login.component';
import { AuthService } from './_services/auth.service';
import { UserListComponent } from './users/user-list/user-list.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { UserComponent } from './users/user/user.component';
import { AlertifyService } from './_services/alertify.service';
import { BsDropdownModule, BsModalService, ModalModule } from 'ngx-bootstrap';
import { MeetingComponent } from './meetings/meeting/meeting.component';
import { MeetingListComponent } from './meetings/meeting-list/meeting-list.component';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    ChangePasswordComponent,
    LoginComponent,
    ForgetPasswordComponent,

    UserListComponent,
    UserComponent,

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
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    AuthService,
    AlertifyService,
    ErrorInterceptorProvider,
    BsModalService,
    AuthGuard
  ],
  entryComponents: [
    ChangePasswordComponent,
    UserComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
