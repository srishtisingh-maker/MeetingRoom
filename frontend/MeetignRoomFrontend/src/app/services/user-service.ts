import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = `${environment.apiBaseUrl}/User`;

  constructor(private http: HttpClient) {}

  updateProfile(formData: FormData): Observable<any> {
    return this.http.put(`${this.baseUrl}/profile`, formData);
  }

  changePassword(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/change-password`, data);
  }

  getProfile(): Observable<any> {
    return this.http.get(`${this.baseUrl}/me`);
  }
}