import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { PagedUser } from '../_models/PagedUser';
import { User } from '../_models/User';

const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'bearer ' + localStorage.getItem('token')
  })
};

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getUsers(): Observable<PagedUser> {
    debugger;
    return this.http.get<PagedUser>(this.baseUrl + 'users', httpOptions);
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id, httpOptions);
  }

  add(model) {
    return this.http.post(this.baseUrl, model);
  }

  edit(model) {
    return this.http.put(this.baseUrl, model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl);
  }
}
