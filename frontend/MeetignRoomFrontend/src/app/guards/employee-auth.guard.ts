import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class EmployeeAuthGuard implements CanActivate {

  constructor(
    private auth: AuthService,
    private router: Router
  ) {}

  canActivate(): boolean {

    // 1️⃣ Login check
    if (!this.auth.isLoggedIn()) {
      this.router.navigate(['/login']);
      return false;
    }

    // 2️⃣ Role check
    if (this.auth.getRole() === 'Employee') {
      return true;
    }

    // Logged in but wrong role
    this.router.navigate(['/403']);
    return false;
  }
}
