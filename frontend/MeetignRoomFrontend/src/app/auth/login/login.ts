import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule,RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
 model = {
    email: '',
    password: ''
  };

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  login() {
    this.authService.login(this.model).subscribe({
      next: (res: any) => {
        console.log('✅ Login response from backend:', res);
        localStorage.setItem('token', res.token);
        localStorage.setItem('role', res.role); 
        localStorage.setItem('userName', res.userName);
        // console.log(res.token);
        // console.log(res.role);
        

        if (res.role === 'Admin') {
          this.router.navigate(['/admin/dashboard']);
        } else {
          this.router.navigate(['/employee/dashboard']);
        }
      },
      error: err => {
        alert(err?.error?.message || 'Login failed');
      }
    });
  }
}