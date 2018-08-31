import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { UsersService } from '../../_services/users.service';
import { UserComponent } from '../user/user.component';
import { AlertifyService } from '../../_services/alertify.service';
import { User } from '../../_models/User';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  bsModalRef: BsModalRef;
  currentPage: number;
  page: number;
  totalItems: number;
  userList: any = [];

  constructor(
    private usersService: UsersService,
    private alertify: AlertifyService,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.usersService.getUsers().subscribe(
      result => {
        this.userList = result.data;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  pageChanged(event: any): void {
    this.page = event.page;
    this.alertify.message(this.page.toString());
  }

  viewUser(user: User) {
    this.showUser(user.id, false);
  }

  addUser() {
    this.showUser(0, true);
  }

  editUser(user: User) {
    this.showUser(user.id, true);
  }

  deleteUser(user: User) {
    this.alertify.confirm('Do you want to delet this user?', () => {
      this.usersService.delete(user.id).subscribe(
        () => {
          this.alertify.message('Delete success');
        },
        error => {
          this.alertify.error(error);
        }
      );
    });
  }

  showUser(userId: number, isEditable: boolean) {
    this.bsModalRef = this.modalService.show(UserComponent);
    this.bsModalRef.content.setModel(userId, isEditable);
  }
}
