import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';
  jwtHtlper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user);
          this.decodedToken = this.jwtHtlper.decodeToken(user.token);
        }
      })
    );
  }

  forgetPassword(email: string) {
    return this.http
      .post(this.baseUrl + 'forgetPassword', email)
      .pipe(map((response: any) => {}));
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHtlper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem('token');
  }
}
