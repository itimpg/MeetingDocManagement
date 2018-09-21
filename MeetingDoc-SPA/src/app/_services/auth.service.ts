import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
import { ChangePasswordModel } from '../_models/ChangePasswordModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.baseUrl + 'auth/';
  jwtHtlper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHtlper.decodeToken(user.token);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }

  changePassword(id: number, model: ChangePasswordModel) {
    return this.http.put(this.baseUrl + 'changePassword/' + id, model);
  }

  forgetPassword(email: string) {
    return this.http.put(`${this.baseUrl}resetpassword`, { email: email });
  }

  renewToken() {
    const token = localStorage.getItem('token');
    this.decodedToken = this.jwtHtlper.decodeToken(token);
    const userId = this.decodedToken.nameid;

    this.http
      .post(this.baseUrl + 'renewal', { userId: userId })
      .subscribe((success: any) => {
        const user = success;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHtlper.decodeToken(user.token);
        }
      });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHtlper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem('token');
  }
}
