import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user-service';
import { CommonModule, DatePipe, NgClass, NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-view-users',
  imports: [DatePipe,NgFor,FormsModule,NgClass,NgIf,CommonModule],
  templateUrl: './view-users.html',
  styleUrl: './view-users.css',
})
export class ViewUsers implements OnInit {

  users: any[] = [];
  filteredUsers: any[] = []; 
  includeAdmins = false;
  loading = false;

  constructor(private userService: UserService,private cdr:ChangeDetectorRef) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.loading = true;

    this.userService.getAllUsers().subscribe({
      next: res => {
        this.users = res;
        this.applyFilter();
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: err => {
        alert('Failed to load users');
        console.error(err);
        this.loading = false;
      }
    });
  }
   applyFilter() {
    if (this.includeAdmins) {
      this.filteredUsers = this.users;
    } else {
      this.filteredUsers = this.users.filter(u => u.role !== 'Admin');
    }
  }

  toggleAdmins() {
    this.includeAdmins = !this.includeAdmins;
    this.applyFilter();
  }
}
