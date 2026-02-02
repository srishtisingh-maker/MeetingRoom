import { CommonModule, NgClass, NgFor, NgIf } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { BookingService } from '../../services/booking-service';

@Component({
  selector: 'app-admin-bookings',
  imports: [NgFor,FormsModule,NgClass,NgIf,CommonModule],
  templateUrl: './admin-bookings.html',
  styleUrl: './admin-bookings.css',
})
export class AdminBookings implements OnInit {
  bookings: any[] = [];

  pendingBookings: any[] = [];
  approvedBookings: any[] = [];
  cancelledBookings: any[] = [];

  selectedBooking: any = null;

  constructor(private bookingService: BookingService,private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    this.loadBookings();
  }

  loadBookings() {
    this.bookingService.getAllBookings().subscribe(res => {
      this.bookings = res;
      this.splitBookings();
      this.cdr.detectChanges();
    });
  }

  splitBookings() {
    this.pendingBookings = this.bookings
      .filter(b => b.status === 'Pending')
      .sort((a, b) => b.id - a.id); // newest first

    this.approvedBookings = this.bookings
      .filter(b => b.status === 'Approved');

    this.cancelledBookings = this.bookings
      .filter(b => b.status === 'Rejected' || b.status === 'Cancelled');
  }

  edit(booking: any) {
    this.selectedBooking = { ...booking };
  }

  update() {
    this.bookingService.updateByAdmin(this.selectedBooking.id, this.selectedBooking)
      .subscribe(() => {
        alert('Booking updated');
        this.selectedBooking = null;
        this.loadBookings();
      });
  }

  approve(id: number) {
    this.bookingService.approve(id).subscribe({
      next: () => this.loadBookings(),
      error: err => {
        alert(err?.error?.message || 'Approval failed');
      }
    });
  }

  reject(id: number) {
    this.bookingService.reject(id).subscribe(() => this.loadBookings());
  }
}