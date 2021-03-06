import { Component, OnInit, TemplateRef } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ChangePasswordComponent } from '../change-password/change-password.component';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any;
  role: string;
  modalRef: BsModalRef;
  isCollapsed = true;

  constructor(
    public authService: AuthService,
    private router: Router,
    private modalService: BsModalService
  ) {
    this.role = this.authService.decodedToken.role;
  }

  ngOnInit() {
    this.model = {
      currentUser: {
        name: 'No name'
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

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
}
