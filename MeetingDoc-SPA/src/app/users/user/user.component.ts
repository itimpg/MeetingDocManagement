import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  title: string;
  model: any = {};

  constructor() { }

  ngOnInit() {
    this.title = 'Add User';
  }

  saveUser() {

  }

}
