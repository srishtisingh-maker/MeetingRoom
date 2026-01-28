import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeMeetingRoom } from './employee-meeting-room';

describe('EmployeeMeetingRoom', () => {
  let component: EmployeeMeetingRoom;
  let fixture: ComponentFixture<EmployeeMeetingRoom>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeMeetingRoom]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeMeetingRoom);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
