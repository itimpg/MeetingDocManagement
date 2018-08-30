import { Component, OnInit, Input } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { User } from '../../_models/User';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  originModel: User;
  model: User = new User();
  title: string;
  isEditable: boolean;

  constructor(public bsModalRef: BsModalRef) {}

  ngOnInit() {}

  setModel(model: User, isEditable: boolean) {
    this.isEditable = isEditable;
    if (!model) {
      this.title = 'Add User';
      model = new User();
    } else {
      this.title = this.isEditable ? 'Edit User' : 'View User';
    }

    this.originModel = model;
    this.model = Object.assign({}, model);
  }

  saveUser() {
    this.originModel = this.model;
  }
}
