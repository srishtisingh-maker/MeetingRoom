import { ChangeDetectorRef, Component } from '@angular/core';
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
  constructor(private authService: AuthService,private router: Router,private cdr:ChangeDetectorRef){}

  register(){
    this.authService.register(this.model).subscribe({
      next: () => {
        alert('Registration successful');
        console.log("registration successful");
        
        this.router.navigate(['/login']);
      },
      error: err => {
        console.log("registration failed due to some error ", err);
        
         err?.error?.message || 'Registration failed. Please try again.';
      alert(err.error.message);
      }
    });
  }
}