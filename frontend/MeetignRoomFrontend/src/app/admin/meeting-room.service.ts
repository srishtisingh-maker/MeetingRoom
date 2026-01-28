import { Injectable } from '@angular/core';
import { environment } from '../environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MeetingRoomService {

  private baseUrl = `${environment.apiBaseUrl}/MeetingRoom`;

  constructor(private http: HttpClient) {}

  private getAuthHeaders() {
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('token')}`
      })
    };
  }
// GET BY ID (ADD THIS)
 getById(id: number): Observable<any> {
  return this.http.get<any>(
    `${this.baseUrl}/${id}`,
    this.getAuthHeaders()
  );
}
  getAll() {
    return this.http.get<any[]>(this.baseUrl, this.getAuthHeaders());
  }

  update(id: number, data: any) {
    return this.http.put(`${this.baseUrl}/${id}`, data, this.getAuthHeaders());
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`, this.getAuthHeaders());
  }
   getAccessories() {
    return this.http.get<any[]>(`${environment.apiBaseUrl}/Accessory`);
  }

  getActiveRooms() {
  return this.http.get<any[]>(
    `${environment.apiBaseUrl}/MeetingRoom/active`,
    this.getAuthHeaders()
  );
}

}
