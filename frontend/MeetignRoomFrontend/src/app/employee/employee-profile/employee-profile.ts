import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { UserService } from '../../services/user-service';
import { CommonModule, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-profile',
  imports: [NgIf,FormsModule,CommonModule],
  templateUrl: './employee-profile.html',
  styleUrl: './employee-profile.css',
})
export class EmployeeProfile {
  
  profile = {
    name: '',
    email: ''
  };

  selectedFile: File | null = null;
  previewUrl: string | null = null;
  loading = false;

  constructor(private userService: UserService,private cdr: ChangeDetectorRef,private router: Router) {}

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (!file) return;

    // File type validation
    if (!['image/png', 'image/jpeg'].includes(file.type)) {
      alert('Only JPG or PNG images are allowed');
      return;
    }

    // File size validation (5MB)
    if (file.size > 5 * 1024 * 1024) {
      alert('Image size must be less than 5MB');
      return;
    }

    // Resolution validation
    const img = new Image();
    img.src = URL.createObjectURL(file);

    img.onload = () => {
      if (img.width < 300 || img.width > 3000 || img.height < 300 || img.height > 3000) {
        alert('Image must be between 300 × 300 and 3000 × 3000 pixels');
        this.selectedFile = null;
        return;
      }

      this.selectedFile = file;

      // Preview
      const reader = new FileReader();
      reader.onload = () => this.previewUrl = reader.result as string;
      reader.readAsDataURL(file);
    };
  }

updateProfile() {
  const formData = new FormData();

  if (this.profile.name?.trim()) {
    formData.append('name', this.profile.name);
  }

  if (this.profile.email?.trim()) {
    formData.append('email', this.profile.email);
  }

  if (this.selectedFile) {
    formData.append('profileImage', this.selectedFile);
  }

  this.loading = true;

  this.userService.updateProfile(formData).subscribe({
    next: res => {
      this.profile = res.user; // backend returns full user
      alert('Profile updated successfully');
      this.loading = false;
      this.cdr.detectChanges();
      this.router.navigate(['/employee/dashboard']);
    },
    error: err => {
      alert(err.error?.message || 'Profile update failed');
      this.loading = false;
    }
  });
}

}

