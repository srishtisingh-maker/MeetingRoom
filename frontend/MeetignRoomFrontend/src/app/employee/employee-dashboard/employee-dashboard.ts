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
  userName = 'User';
  Id!: number;
  name!: string;
  email!: string;
  Role!: string;
  ProfilePic!: string;

  constructor(private meetingRoomService: MeetingRoomService,private http: HttpClient, private router:Router,private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    // Read username saved during login
   
    // this.userName = localStorage.getItem('userName') || 'User';

    const userData = localStorage.getItem('user');
    if (userData) {
      const user = JSON.parse(userData);
      this.Id = user.id;
      this.name = user.name;
      this.email = user.email;
      this.Role = user.role;
      this.ProfilePic = user.profileImageUrl;

      // console.log("User Data:", user);
      // console.log("Profile Pic URL:", this.ProfilePic);
      // console.log("Name:", this.name);
      // console.log("Email:", this.email);
      // console.log("Role:", this.Role);
      // console.log("Id:", this.Id);

      this.cdr.detectChanges();
    }
  }

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
    localStorage.removeItem('userName');
    this.router.navigate(['/login']);
  }
}
