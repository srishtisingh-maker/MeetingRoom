// import { HttpClient } from '@angular/common/http';
// import { ChangeDetectorRef, Component } from '@angular/core';
// import { UserService } from '../../services/user-service';
// import { CommonModule, NgIf } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { Router } from '@angular/router';

// @Component({
//   selector: 'app-employee-profile',
//   imports: [NgIf,FormsModule,CommonModule],
//   templateUrl: './employee-profile.html',
//   styleUrl: './employee-profile.css',
// })
// export class EmployeeProfile {
  
//   profile = {
//     name: '',
//     email: ''
//   };

//   selectedFile: File | null = null;
//   previewUrl: string | null = null;
//   loading = false;
//  Id!: number;
//   name!: string;
//   email!: string;
//   Role!: string;
//   ProfilePic!: string;
//   constructor(private userService: UserService,private cdr: ChangeDetectorRef,private router: Router) {}
// ngOnInit() {
 
//     const userData = localStorage.getItem('user');
//     if (userData) {
//       const user = JSON.parse(userData);
//       this.Id = user.id;
//       this.name = user.name;
//       this.email = user.email;
//       this.Role = user.role;
//       this.ProfilePic = user.profileImageUrl;

//       // console.log("User Data:", user);
//       // console.log("Profile Pic URL:", this.ProfilePic);
//       // console.log("Name:", this.name);
//       // console.log("Email:", this.email);
//       // console.log("Role:", this.Role);
//       // console.log("Id:", this.Id);

//       this.cdr.detectChanges();
//     }

//     this.profile.name = this.name;
//     this.profile.email = this.email;
//   }
//   onFileSelected(event: any) {
//     const file: File = event.target.files[0];
//     if (!file) return;

//     // File type validation
//     if (!['image/png', 'image/jpeg'].includes(file.type)) {
//       alert('Only JPG or PNG images are allowed');
//       return;
//     }

//     // File size validation (5MB)
//     if (file.size > 5 * 1024 * 1024) {
//       alert('Image size must be less than 5MB');
//       return;
//     }

//     // Resolution validation
//     const img = new Image();
//     img.src = URL.createObjectURL(file);

//     img.onload = () => {
//       if (img.width < 300 || img.width > 3000 || img.height < 300 || img.height > 3000) {
//         alert('Image must be between 300 × 300 and 3000 × 3000 pixels');
//         this.selectedFile = null;
//         return;
//       }

//       this.selectedFile = file;

//       // Preview
//       const reader = new FileReader();
//       reader.onload = () => this.previewUrl = reader.result as string;
//       reader.readAsDataURL(file);
//     };
//   }

// updateProfile() {
//   const formData = new FormData();

//   if (this.profile.name?.trim()) {
//     formData.append('name', this.profile.name);
//   }

//   if (this.profile.email?.trim()) {
//     formData.append('email', this.profile.email);
//   }

//   if (this.selectedFile) {
//     formData.append('profileImage', this.selectedFile);
//   }

//   formData.append('Id', this.Id.toString());

//   console.log('Form Data:', formData);

//   this.loading = true;

//   this.userService.updateProfile(formData).subscribe({
//     next: res => {
//       this.profile = res.user; // backend returns full user
//       alert('Profile updated successfully');
//       this.loading = false;
//       this.cdr.detectChanges();
//       this.router.navigate(['/employee/dashboard']);
//     },
//     error: err => {
//       alert(err.error?.message || 'Profile update failed');
//       this.loading = false;
//     }
//   });
// }

// }



import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user-service';

@Component({
  selector: 'app-employee-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employee-profile.html',
  styleUrl: './employee-profile.css'
})
export class EmployeeProfile {

  profileForm!: FormGroup;

  selectedFile: File | null = null;
  previewUrl: string | null = null;
  loading = false;

  ProfilePic!: string;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {}

  ngOnInit() {
    const userData = localStorage.getItem('user');

    let name = '';
    let email = '';

    if (userData) {
      const user = JSON.parse(userData);
      name = user.name || '';
      email = user.email || '';
      this.ProfilePic = user.profileImageUrl;
    }

    this.buildForm(name, email);
  }

  private buildForm(name: string, email: string) {
    this.profileForm = this.fb.group({
      name: [name, [Validators.maxLength(100)]],
      email: [email, [Validators.email]]
    });
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files || !input.files.length) return;

    const file = input.files[0];

    // type validation
    if (!['image/png', 'image/jpeg'].includes(file.type)) {
      alert('Only JPG or PNG images are allowed');
      return;
    }

    // size validation (5MB)
    if (file.size > 5 * 1024 * 1024) {
      alert('Image size must be less than 5MB');
      return;
    }

    const img = new Image();
    img.src = URL.createObjectURL(file);

    img.onload = () => {
      if (
        img.width < 300 || img.width > 3000 ||
        img.height < 300 || img.height > 3000
      ) {
        alert('Image must be between 300×300 and 3000×3000');
        return;
      }

      this.selectedFile = file;

      const reader = new FileReader();
      reader.onload = () => this.previewUrl = reader.result as string;
      reader.readAsDataURL(file);
    };
  }

  // updateProfile() {
  //   if (this.profileForm.invalid) {
  //     this.profileForm.markAllAsTouched();
  //     return;
  //   }

  //   const formData = new FormData();
  //   const { name, email } = this.profileForm.value;

  //   if (name?.trim()) formData.append('name', name);
  //   if (email?.trim()) formData.append('email', email);
  //   if (this.selectedFile) formData.append('profileImage', this.selectedFile);

  //   this.loading = true;

  //   this.userService.updateProfile(formData).subscribe({
  //     next: (res: any) => {
  //       alert('Profile updated successfully');

  //       const userData = localStorage.getItem('user');
  //       if (userData) {
  //         const user = JSON.parse(userData);

  //         user.name = name;
  //         user.email = email;

  //         if (res.profileImageUrl) {
  //           this.ProfilePic = `${res.profileImageUrl}?v=${Date.now()}`;
  //           user.profileImageUrl = this.ProfilePic;
  //         }

  //         localStorage.setItem('user', JSON.stringify(user));

  //         //  notify navbar/dashboard
  //         window.dispatchEvent(new Event('userUpdated'));
  //       }

  //       this.previewUrl = null;
  //       this.selectedFile = null;
  //       this.loading = false;
  //       this.cdr.detectChanges();
  //     },
  //     error: err => {
  //       alert(err.error?.message || 'Profile update failed');
  //       this.loading = false;
  //     }
  //   });
  // }

  updateProfile() {
  if (this.profileForm.invalid) {
    this.profileForm.markAllAsTouched();
    return;
  }

  const userData = localStorage.getItem('user');
  if (!userData) {
    alert('User not found. Please login again.');
    return;
  }

  const user = JSON.parse(userData);

  const formData = new FormData();
  const { name, email } = this.profileForm.value;

  formData.append('id', user.id.toString());

  if (name?.trim()) formData.append('name', name);
  if (email?.trim()) formData.append('email', email);
  if (this.selectedFile) {
    formData.append('profileImage', this.selectedFile);
  }

  this.loading = true;

  this.userService.updateProfile(formData).subscribe({
    next: (res: any) => {
      alert('Profile updated successfully');

      user.name = name;
      user.email = email;

      if (res.user?.profileImageUrl) {
        this.ProfilePic = `${res.user.profileImageUrl}?v=${Date.now()}`;
        user.profileImageUrl = this.ProfilePic;
      }

      localStorage.setItem('user', JSON.stringify(user));
      window.dispatchEvent(new Event('userUpdated'));

      this.previewUrl = null;
      this.selectedFile = null;
      this.loading = false;
      this.cdr.detectChanges();
    },
    error: err => {
      alert(err.error?.message || 'Profile update failed');
      this.loading = false;
    }
  });
}


  get name() {
    return this.profileForm.get('name');
  }

  get email() {
    return this.profileForm.get('email');
  }
}