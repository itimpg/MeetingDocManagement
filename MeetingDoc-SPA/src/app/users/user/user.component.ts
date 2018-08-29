import { Component, OnInit, Input } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  model: any;
  title: string;
  isEditable: boolean;

  constructor(public bsModalRef: BsModalRef) {}

  ngOnInit() {
    if (this.model) {
      this.title = 'Edit User';
    } else {
      this.title = 'Add User';
      this.model = {};
    }
  }

  saveUser() {}
}
