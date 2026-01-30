import { CommonModule, NgClass, NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
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
  selectedBooking: any = null;

  constructor(private bookingService: BookingService) {}

  ngOnInit() {
    this.loadBookings();
  }

  loadBookings() {
    this.bookingService.getAllBookings().subscribe(res => {
      this.bookings = res;
    });
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
    this.bookingService.approve(id).subscribe(() => this.loadBookings());
  }

  reject(id: number) {
    this.bookingService.reject(id).subscribe(() => this.loadBookings());
  }
}
