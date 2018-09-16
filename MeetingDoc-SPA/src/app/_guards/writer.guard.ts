import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class WriterGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      const role = this.authService.decodedToken.role;
      if (role === '0' || role === '1') {
        return true;
      } else {
        this.router.navigate(['home']);
        return false;
      }
    }

    this.router.navigate(['/login']);
    return false;
  }
}
