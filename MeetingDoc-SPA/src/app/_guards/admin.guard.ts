import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      if (this.authService.decodedToken.role === '0') {
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
