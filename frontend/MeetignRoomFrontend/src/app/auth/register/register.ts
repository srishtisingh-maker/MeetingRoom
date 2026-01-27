import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  imports: [FormsModule,RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  model={
    name:'',
    email:'',
    password:'',
    role:'Employee'
  };
  constructor(private authService: AuthService,private router: Router){}

  register(){
    this.authService.register(this.model).subscribe({
      next: () => {
        alert('Registration successful');
        this.router.navigate(['/login']);
      },
      error: err => {
         err?.error?.message || 'Registration failed. Please try again.';
      alert(err.error.message);
      }
    });
  }
}