import { Routes } from '@angular/router';
import { UserListComponent } from './users/user-list/user-list.component';
import { LoginComponent } from './login/login.component';
import { MeetingListComponent } from './meetings/meeting-list/meeting-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';

export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'forgetpassword', component: ForgetPasswordComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    // canActivate: [AuthGuard],
    children: [
      { path: '', component: MeetingListComponent },
      { path: 'users', component: UserListComponent }
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
