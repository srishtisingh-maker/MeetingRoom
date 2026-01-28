
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { environment } from '../../environment';
import { MeetingRoomService } from '../meeting-room.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-meeting-rooms',
  imports: [NgIf],
  templateUrl: './meeting-rooms.html',
  styleUrl: './meeting-rooms.css',
})

export class MeetingRooms implements OnInit  {
 rooms: any[] = [];
loading = true;
isAdmin = false;
  constructor(private meetingRoomService: MeetingRoomService,private http: HttpClient, private router:Router,private cdr: ChangeDetectorRef) {}

 ngOnInit() {

  this.isAdmin = localStorage.getItem('role') === 'Admin';
  console.log(this.isAdmin);
  
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
  
  editRoom(room: any) {
  this.router.navigate(['/admin/dashboard/meeting-rooms/edit', room.id]);
  }

  deactivateRoom(room: any) {
  const confirmed = confirm(
    'Are you sure you want to deactivate this room?'
  );

  if (!confirmed) return;

  this.meetingRoomService.delete(room.id).subscribe({
    next: () => {
      room.isActive = false; // instant UI update
      alert('Meeting room deactivated');
      this.cdr.detectChanges();
    },
    error: () => {
      alert('Failed to deactivate room');
    }
  });
}



}
 