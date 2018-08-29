import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseUrl = environment.baseUrl + 'api/users/';

  constructor(private http: HttpClient) {}

  get() {
    return this.http.get(this.baseUrl);
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
