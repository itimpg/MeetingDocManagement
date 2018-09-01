import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { ChangePasswordModel } from '../_models/ChangePasswordModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  model: ChangePasswordModel;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router,
    public bsModalRef: BsModalRef
  ) {
    this.model = new ChangePasswordModel();
  }

  ngOnInit() {}

  changePassword() {
    if (this.model.newPassword !== this.model.renewPassword) {
      this.alertify.error('new password are mismatched.');
    } else {
      const userId = this.authService.decodedToken.nameid;
      this.authService.changePassword(userId, this.model).subscribe(
        reponse => {
          this.alertify.success('Password was changed');
          this.authService.logout();
          this.bsModalRef.hide();
          this.router.navigate(['/login']);
        },
        error => {
          this.alertify.error(error);
        }
      );
    }
  }
}
