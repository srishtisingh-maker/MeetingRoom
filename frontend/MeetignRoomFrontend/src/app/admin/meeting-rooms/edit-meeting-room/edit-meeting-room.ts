import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MeetingRoomService } from '../../meeting-room.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-meeting-room',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-meeting-room.html',
  styleUrl: './edit-meeting-room.css',
})
export class EditMeetingRoom implements OnInit {

  roomId!: number;
  roomForm!: FormGroup;
  accessories: any[] = [];
  loading = false;
  submitting = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private meetingRoomService: MeetingRoomService,
  private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.roomId = Number(this.route.snapshot.paramMap.get('id'));

    this.roomForm = this.fb.group({
      name: ['', Validators.required],
      building: ['', Validators.required],
      floor: ['', [Validators.required, Validators.min(0)]],
      capacity: ['', [Validators.required, Validators.min(1)]],
      isActive: [true],
      accessoryIds: [[]] 
    });
    this.loadAccessories();
    this.loadRoom();
  }
   loadAccessories() {
    this.meetingRoomService.getAccessories().subscribe({
      next: (data) => {
        this.accessories = data,
        this.cdr.detectChanges(); 
      },
      error: () => alert('Failed to load accessories')
    });
  }
  loadRoom() {
    this.loading = true;

    this.meetingRoomService.getById(this.roomId).subscribe({
      next: (room) => {
        this.roomForm.patchValue({
          name: room.name,
          building: room.building,
          floor: room.floor,
          capacity: room.capacity,
          isActive: room.isActive,
          accessoryIds: (room.roomAccessories ?? []).map(
          (ra: any) => ra.accessory.id
          )
      });

        this.loading = false;
        
      this.cdr.detectChanges();
      },
      error: () => {
        alert('Failed to load meeting room');
        this.router.navigate(['/admin/dashboard/meeting-rooms']);
      }
    });
  }

  submit() {
  if (this.roomForm.invalid) {
    this.roomForm.markAllAsTouched();
    return;
  }

  this.submitting = true;

  const payload = {
    ...this.roomForm.value,
    accessoryIds: this.roomForm.value.accessoryIds.map((id: string) => +id)
  };
  
  this.meetingRoomService.update(this.roomId, payload).subscribe({
    next: () => {
      alert('Meeting room updated successfully');
      this.submitting = false;
      this.router.navigate(['/admin/dashboard/meeting-rooms']);
    },
    error: err => {
      this.submitting = false;
      alert(err?.error?.message || 'Update failed');
    }
  });
}

  cancel() {
    this.router.navigate(['/admin/dashboard/meeting-rooms']);
  }

  onAccessoryChange(event: any) {
  const selected = this.roomForm.value.accessoryIds as number[];

  if (event.target.checked) {
    selected.push(+event.target.value);
  } else {
    const index = selected.indexOf(+event.target.value);
    if (index > -1) selected.splice(index, 1);
  }

  this.roomForm.patchValue({ accessoryIds: selected });
}

}