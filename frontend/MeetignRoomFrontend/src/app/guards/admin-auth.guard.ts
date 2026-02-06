import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth.service';
@Injectable({ providedIn: 'root' })
export class AdminAuthGuard implements CanActivate {

  constructor(
    private auth: AuthService,
    private router: Router
  ) {}

  canActivate(): boolean {

    // Login check
    if (!this.auth.isLoggedIn()) {
      this.router.navigate(['/login']);
      return false;
    }

    // Role check
    if (this.auth.getRole() === 'Admin') {
      return true;
    }

    // Logged in but wrong role
    this.router.navigate(['/403']);
    return false;
  }
}
