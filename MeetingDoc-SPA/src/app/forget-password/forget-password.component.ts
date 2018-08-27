import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css']
})
export class ForgetPasswordComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService) {}

  ngOnInit() {}

  forgetPassword() {
    this.authService.forgetPassword(this.model.username).subscribe(
      response => {
        console.log('request password');
      },
      error => {
        console.log('Request error');
      }
    );
  }
}
