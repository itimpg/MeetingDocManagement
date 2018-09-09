import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService<T> {
  protected abstract action: string;

  protected baseUrl: string = environment.baseUrl;

  constructor(protected http: HttpClient) {}

  getItems(page?, itemsPerPage?): Observable<PaginatedResult<T[]>> {
    const paginatedResult: PaginatedResult<T[]> = new PaginatedResult<T[]>();
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<any>(this.baseUrl + this.action, { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return paginatedResult;
        })
      );
  }

  getItem(id: number): Observable<T> {
    return this.http.get<T>(this.baseUrl + `${this.action}/` + id);
  }

  add(model: T) {
    return this.http.post(`${this.baseUrl}${this.action}/`, model);
  }

  edit(model: T) {
    return this.http.put(`${this.baseUrl}${this.action}/`, model);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}${this.action}/${id}`);
  }
}
