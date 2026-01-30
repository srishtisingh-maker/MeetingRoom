import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeBookings } from './employee-bookings';

describe('EmployeeBookings', () => {
  let component: EmployeeBookings;
  let fixture: ComponentFixture<EmployeeBookings>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeBookings]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeBookings);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
