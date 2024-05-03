import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { PaginationResult } from 'src/app/models/PaginationResult';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  constructor(private http: HttpClient) {}

  create(url: string, data: any, content: string, authorization = '') {
    let headers = this.setHeaders(content, authorization);
    return this.http
      .post(environment.siteUrl + url, data, { headers: headers })
      .pipe(map((response) => response));
  }

  get(url: string, contentType: string, authorization?: any) {
    let headers = this.setHeaders(contentType, authorization);
    return this.http
      .get(environment.siteUrl + url, { headers: headers })
      .pipe(map((response) => response));
  }
  getWithPagination(
    url: string,
    contentType: string,
    authorization = '',
    pageNumber?: any,
    pageSize?: any,
    param?: any
  ): PaginationResult | any {
    debugger;

    let headers = this.setHeaders(contentType, authorization);

    if (pageNumber != null && pageSize != null) {
      let data = {
        pageNumber: pageNumber,
        pageSize: pageSize,
      };

      if (param != null) {
        data = Object.assign(data, param);
      }

      return this.http
        .post(environment.siteUrl + url, data, {
          headers: headers,
        })
        .pipe(map((response) => response));
    }
    return this.http
      .post(environment.siteUrl + url, { headers: headers })
      .pipe(map((response) => response));
  }
  update(url: string, data: any, contentType: string, authorization = '') {
    let headers = this.setHeaders(contentType, authorization);
    return this.http
      .put(environment.siteUrl + url, data, { headers: headers })
      .pipe(map((response) => response));
  }

  delete(url: string, contentType: string, authorization = '') {
    let headers = this.setHeaders(contentType, authorization);
    return this.http
      .delete(environment.siteUrl + url, { headers: headers })
      .pipe(map((response) => response));
  }

  setHeaders(content: string, auth: string) {
    let headers = new HttpHeaders();
    if (auth) {
      headers = headers
        .set('Content-type', content)
        .set('Authorization', `Bearer ${auth}`);
    } else {
      headers = headers.set('Content-type', content);
    }
    return headers;
  }
}
