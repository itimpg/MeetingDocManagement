import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ChangePasswordComponent } from '../change-password/change-password.component';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any;

  constructor(public authService: AuthService, private router: Router, private modalService: BsModalService) {}

  ngOnInit() {
    this.model = {
      currentUser: {
        name: 'Test'
      }
    };
  }

  showChagnePassword() {
    this.modalService.show(ChangePasswordComponent);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
