import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})

export class AuthService {

  //backend URL
  private baseUrl = ' https://localhost:7217/api/auth'

constructor(private http : HttpClient) {}
 register(data:any)
  {
    return this.http.post(`${this.baseUrl}/register`,data);
  }

  login(data: any) {
    return this.http.post(`${this.baseUrl}/login`, data);
  }
}

