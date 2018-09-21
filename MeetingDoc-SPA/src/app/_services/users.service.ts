import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { User } from '../_models/User';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient, private authService: AuthService) {}

  getUsers(page?, itemsPerPage?): Observable<PaginatedResult<User[]>> {
    const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<
      User[]
    >();
    this.authService.renewToken();
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<any>(this.baseUrl + 'users', {observe: 'response', params})
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }

  getUser(id: number): Observable<User> {
    this.authService.renewToken();
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

  add(model: User) {
    this.authService.renewToken();
    return this.http.post(this.baseUrl + 'auth/Register', {
      userViewModel: model,
      password: model.password
    });
  }

  edit(model: User) {
    this.authService.renewToken();
    return this.http.put(this.baseUrl + 'users/', model);
  }

  delete(id: number) {
    this.authService.renewToken();
    return this.http.delete(this.baseUrl + 'users/' + id);
  }
}
