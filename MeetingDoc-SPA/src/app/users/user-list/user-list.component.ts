import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  userList: any = [];

  constructor() {}

  ngOnInit() {
    this.userList.push({ id: 1, name: 'test 1', email: 'test1@test.com' });
    this.userList.push({ id: 2, name: 'test 2', email: 'test2@test.com' });
    this.userList.push({ id: 3, name: 'test 3', email: 'test3@test.com' });
  }
}
