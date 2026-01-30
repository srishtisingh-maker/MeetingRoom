import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BookingService } from '../../services/booking-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-employee-bookings',
  imports: [FormsModule, CommonModule],
  templateUrl: './employee-bookings.html',
  styleUrl: './employee-bookings.css',
})
export class EmployeeBookings implements OnInit {
  bookings: any[] = [];

  showForm = false;
  isEdit = false;
  editId: number | null = null;

  // Single form model for Create + Edit
  formModel = {
    roomId: 1,
    date: '',
    startTime: '',
    endTime: '',
    participants: 1,
  };

  constructor(
    private bookingService: BookingService,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    this.loadBookings();
  }

  loadBookings() {
    this.bookingService.getMyBookings().subscribe((res) => {
      this.bookings = res;
      this.cdr.detectChanges();
    });
  }

  openCreate() {
    this.isEdit = false;
    this.editId = null;
    this.formModel = {
      roomId: 1,
      date: '',
      startTime: '',
      endTime: '',
      participants: 1,
    };
    this.showForm = true;
  }

  openEdit(b: any) {
    this.isEdit = true;
    this.editId = b.id;
    this.formModel = {
      roomId: b.roomId, // not editable, but retained
      date: b.date,
      startTime: b.startTime,
      endTime: b.endTime,
      participants: b.participants,
    };
    this.showForm = true;
  }

  save() {
    if (this.isEdit && this.editId) {
      this.bookingService
        .updateByEmployee(this.editId, {
          date: this.formModel.date,
          startTime: this.formModel.startTime,
          endTime: this.formModel.endTime,
          participants: this.formModel.participants,
        })
        .subscribe(() => {
          alert('Booking updated');
          console.log(this.formModel.startTime);
      
          this.reset();
        });
    } else {
      const start = new Date(`1970-01-01T${this.formModel.startTime}`);
      const end = new Date(`1970-01-01T${this.formModel.endTime}`);

      if (end <= start) {
        alert('End time must be after start time');
        return;
      }

      const diffMinutes = (end.getTime() - start.getTime()) / (1000 * 60);
      if (diffMinutes < 30) {
        alert('Booking must be at least 30 minutes');
        return;
      }
      this.bookingService.createBooking(this.formModel).subscribe({
        next: (res: any) => {
          alert('Booking created successfully!');
          this.loadBookings(); // refresh list
        },
        error: (err) => {
          // err.error.message comes from your AppException
          alert(err?.error?.message || 'Something went wrong');
        },
      });
    }
  }

  cancelBooking(id: number) {
    if (!confirm('Cancel this booking?')) return;

    this.bookingService.cancel(id).subscribe(() => {
      alert('Booking cancelled');
      this.loadBookings();
    });
  }

  reset() {
    this.showForm = false;
    this.isEdit = false;
    this.editId = null;
    this.loadBookings();
  }
}
