import { Routes } from '@angular/router';
import { Login } from './auth/login/login';
import { Register } from './auth/register/register';
import { AdminDashboard } from './admin/admin-dashboard/admin-dashboard';
import { EmployeeDashboard } from './employee/employee-dashboard/employee-dashboard';
import { MeetingRooms } from './admin/meeting-rooms/meeting-rooms';

export const routes: Routes = [
  { path: '', redirectTo: 'register', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
   {
    path: 'admin/dashboard',
    component: AdminDashboard,
    children: [
      { path: 'meeting-rooms', component: MeetingRooms }
    ]
  },
  { path:'employee/dashboard',component:EmployeeDashboard }

];
