import { Component, OnInit, Input } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { User } from '../../_models/User';
import { UsersService } from '../../_services/users.service';
import { AuthService } from '../../_services/auth.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  model: User;
  title: string;
  isEditable: boolean;

  constructor(
    public bsModalRef: BsModalRef,
    private userService: UsersService,
    private authService: AuthService,
    private alertify: AlertifyService
  ) {
    this.model = new User();
    this.model.id = 0;
  }

  ngOnInit() {}

  setModel(userId: number, isEditable: boolean) {
    if (userId === 0) {
      this.isEditable = true;
      this.title = 'Add User';
    } else {
      this.isEditable = isEditable;
      this.title = this.isEditable ? 'Edit User' : 'View User';
      this.userService.getUser(userId).subscribe(
        user => {
          this.model = user;
        },
        error => {
          this.alertify.error(error);
        }
      );
    }
  }

  saveUser() { 
    if (this.model.id === 0) {
      this.authService.register(this.model).subscribe(
        success => {
          this.alertify.message('save success');
        },
        error => {
          this.alertify.error(error);
        }
      );
    } else {
      this.userService.edit(this.model).subscribe(
        success => {
          this.alertify.message('save success');
        },
        error => {
          this.alertify.error(error);
        }
      );
    }
  }
}
