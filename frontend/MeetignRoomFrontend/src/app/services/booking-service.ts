import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  constructor(private http: HttpClient) {}

  // ADMIN
  getAllBookings(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiBaseUrl}/Booking`);
  }

  updateByAdmin(id: number, data: any) {
    return this.http.put(`${environment.apiBaseUrl}/Booking/admin/${id}`, data);
  }

  approve(id: number) {
    return this.http.post(`${environment.apiBaseUrl}/Booking/${id}/approve`, {});
  }

  reject(id: number) {
    return this.http.post(`${environment.apiBaseUrl}/Booking/${id}/reject`, {});
  }

  // EMPLOYEE
  getMyBookings(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiBaseUrl}/Booking/my`);
  }

  createBooking(data: any) {
    return this.http.post(`${environment.apiBaseUrl}/Booking`, data);
  }

  updateByEmployee(id: number, data: any) {
    return this.http.put(`${environment.apiBaseUrl}/Booking/${id}`, data);
  }

  cancel(id: number) {
    return this.http.delete(`${environment.apiBaseUrl}/Booking/${id}`);
  }
}

