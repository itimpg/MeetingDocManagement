import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { PagedUser } from '../_models/PagedUser';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getUsers(): Observable<PagedUser> {
    return this.http.get<PagedUser>(this.baseUrl + 'users');
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

  add(model: User) {
    return this.http.post(this.baseUrl + 'auth/Register', {
      userViewModel: model,
      password: model.password
    });
  }

  edit(model: User) {
    return this.http.put(this.baseUrl + 'users/', model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + 'users/' + id);
  }
}
