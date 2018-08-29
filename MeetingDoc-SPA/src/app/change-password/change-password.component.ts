import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  model: any = {};

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    public bsModalRef: BsModalRef
  ) {}

  ngOnInit() {}

  changePassword() {
    this.authService.changePassword(this.model).subscribe(
      reponse => {
        this.alertify.success('Password was changed');
        this.authService.logout();
        this.bsModalRef.hide();
      },
      error => {
        this.alertify.error(error);
      }
    );
  }
}
