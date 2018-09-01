import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css']
})
export class ForgetPasswordComponent implements OnInit {
  model: any = {};

  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {}

  forgetPassword() {
    this.authService.forgetPassword(this.model.forgotUsername).subscribe(
      response => {
        this.alertify.message(
          `Your new password was sent to email: ${this.model.forgotUsername}`
        );
        this.router.navigate(['/login']);
      },
      error => {
        this.alertify.error(error);
      }
    );
  }
}
