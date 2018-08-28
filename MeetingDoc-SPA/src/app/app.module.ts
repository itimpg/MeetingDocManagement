import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HttpModule } from '../../node_modules/@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { NavComponent } from './nav/nav.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { AuthService } from './_services/auth.service';
import { AlertComponent } from './alert/alert.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { UserComponent } from './users/user/user.component';
import { AlertifyService } from './_services/alertify.service';
import { BsDropdownModule } from 'ngx-bootstrap';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      ChangePasswordComponent,
      LoginComponent,
      AlertComponent,
      UserListComponent,
      UserComponent,
      ForgetPasswordComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot()
   ],
   providers: [
      AuthService,
      AlertifyService,
      ErrorInterceptorProvider
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
