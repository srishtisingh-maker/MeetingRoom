import { ChangeDetectorRef, Component } from '@angular/core';
import { MeetingRoomService } from '../../admin/meeting-room.service';

@Component({
  selector: 'app-employee-meeting-room',
  imports: [],
  templateUrl: './employee-meeting-room.html',
  styleUrl: './employee-meeting-room.css',
})
export class EmployeeMeetingRoom {
 rooms: any[] = [];
  loading = true;

  constructor(private meetingRoomService: MeetingRoomService,private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.loadRooms();
  }

  loadRooms() {
    this.loading = true;

    this.meetingRoomService.getActiveRooms().subscribe({
      next: res => {
        this.rooms = res;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        alert('Failed to load meeting rooms');
        this.loading = false;
      }
    });
  }
}