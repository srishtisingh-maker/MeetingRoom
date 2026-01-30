import { Routes } from '@angular/router';
import { Login } from './auth/login/login';
import { Register } from './auth/register/register';
import { AdminDashboard } from './admin/admin-dashboard/admin-dashboard';
import { EmployeeDashboard } from './employee/employee-dashboard/employee-dashboard';
import { MeetingRooms } from './admin/meeting-rooms/meeting-rooms';
import { EditMeetingRoom } from './admin/meeting-rooms/edit-meeting-room/edit-meeting-room';
import { EmployeeMeetingRoom } from './employee/employee-meeting-room/employee-meeting-room';
import { AdminBookings } from './admin/admin-bookings/admin-bookings';
import { EmployeeBookings } from './employee/employee-bookings/employee-bookings';
import { EmployeeProfile } from './employee/employee-profile/employee-profile';

export const routes: Routes = [
  { path: '', redirectTo: 'register', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  {
    path: 'admin/dashboard',
    component: AdminDashboard,
    children: [
      { path: 'meeting-rooms', component: MeetingRooms },
      { path: 'meeting-rooms/edit/:id', component: EditMeetingRoom },
      { path: 'bookings', component: AdminBookings }
    ]
  },
  { path:'employee/dashboard',component:EmployeeDashboard,
     children: [
      { path: 'meeting-rooms', component: EmployeeMeetingRoom },
      { path: 'bookings', component: EmployeeBookings },
      { path: 'profile', component: EmployeeProfile}
    ]
  },
  

];
