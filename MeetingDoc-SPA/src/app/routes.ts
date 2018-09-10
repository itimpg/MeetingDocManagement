import { Routes } from '@angular/router';
import { UserListComponent } from './users/user-list/user-list.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_guards/auth.guard';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { UserComponent } from './users/user/user.component';
import { UserDetailResolver } from './_resolvers/user.resolver';
import { UserListResolver } from './_resolvers/userlist.resolver';
import { MeetingTypeListComponent } from './meeting-type/meeting-type-list/meeting-type-list.component';
import { MeetingTypeComponent } from './meeting-type/meeting-type/meeting-type.component';
import { MeetingTypeListResolver } from './_resolvers/meetingtypelist.resolver';
import { MeetingTypeDetailResolver } from './_resolvers/meetingtype.resolver';
import { MeetingTopicListComponent } from './meeting-topic/meeting-topic-list/meeting-topic-list.component';
import { MeetingTopicListResolver } from './_resolvers/meetingtopiclist.resolver';

export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'forgetpassword', component: ForgetPasswordComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        component: MeetingTypeListComponent,
        resolve: { meetingType: MeetingTypeListResolver }
      },
      {
        path: 'meetingTypes/:id',
        component: MeetingTypeComponent,
        resolve: { meetingType: MeetingTypeDetailResolver }
      },
      {
        path: 'meetingTypes/:id/topic',
        component: MeetingTopicListComponent,
        resolve: { meetingTopic: MeetingTopicListResolver }
      },
      {
        path: 'users',
        component: UserListComponent,
        resolve: { user: UserListResolver }
      },
      {
        path: 'users/:id',
        component: UserComponent,
        resolve: { user: UserDetailResolver }
      }
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
