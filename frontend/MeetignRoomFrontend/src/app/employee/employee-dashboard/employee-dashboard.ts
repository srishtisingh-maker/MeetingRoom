import { ChangeDetectorRef, Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { MeetingRoomService } from '../../admin/meeting-room.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-employee-dashboard',
  imports: [RouterOutlet,RouterLink],
  templateUrl: './employee-dashboard.html',
  styleUrl: './employee-dashboard.css',
})
export class EmployeeDashboard {
  loading = false;
  rooms: any[] = [];
  
  constructor(private meetingRoomService: MeetingRoomService,private http: HttpClient, private router:Router,private cdr: ChangeDetectorRef) {}
  
  loadRooms() {
  this.loading = true;

  this.meetingRoomService.getActiveRooms().subscribe({
    next: res => {
      this.rooms = res;
      this.loading = false;
    },
    error: () => {
      alert('Failed to load meeting rooms');
      this.loading = false;
    }
  });
}

 logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.router.navigate(['/login']);
  }
}
