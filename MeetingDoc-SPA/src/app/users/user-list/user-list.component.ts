import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { UsersService } from '../../_services/users.service';
import { UserComponent } from '../user/user.component';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  bsModalRef: BsModalRef;
  currentPage: number;
  page: number;
  totalItems: number = 66;
  userList: any = [];

  constructor(
    private usersService: UsersService,
    private alertify: AlertifyService,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.userList.push({ id: 1, name: 'test 1', email: 'test1@test.com' });
    this.userList.push({ id: 2, name: 'test 2', email: 'test2@test.com' });
    this.userList.push({ id: 3, name: 'test 3', email: 'test3@test.com' });

    // this.usersService.get();
  }

  pageChanged(event: any): void {
    this.page = event.page;
    this.alertify.message(this.page.toString());
  }

  viewUser(user) {
    this.alertify.message('view');
    this.showUser(user, false);
  }

  addUser() {
    this.alertify.message('add');
    this.showUser(null, true);
  }

  editUser(user) {
    this.alertify.message('edit');
    this.showUser(user, true);
  }

  deleteUser(user) {
    this.alertify.confirm('Do you want to delet this user?', () => {
      this.usersService.delete(user.id);
    });
  }

  showUser(model: any, isEditable: boolean) {
    this.bsModalRef = this.modalService.show(UserComponent);
    this.bsModalRef.content.model = model;
    this.bsModalRef.content.isEditable = isEditable;
  }
}
