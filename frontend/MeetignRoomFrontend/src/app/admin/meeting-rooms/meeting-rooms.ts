
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

import { RouterLink, RouterOutlet } from '@angular/router';
import { environment } from '../../environment';

@Component({
  selector: 'app-meeting-rooms',
  imports: [RouterOutlet,RouterLink],
  templateUrl: './meeting-rooms.html',
  styleUrl: './meeting-rooms.css',
})
export class MeetingRooms implements OnInit  {
 rooms: any[] = [];
loading = true;

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

 ngOnInit() {
    this.loadRooms();
  }

  loadRooms() {
    this.loading = true;

    this.http.get<any[]>(`${environment.apiBaseUrl}/MeetingRoom`).subscribe({
        next: res => {
          this.rooms = res;
          this.loading = false;

          // FORCE UI REFRESH
          this.cdr.detectChanges();
        },
        error: err => {
          console.error(err);
          this.loading = false;
          this.cdr.detectChanges();
        }
      });
  }
}
 