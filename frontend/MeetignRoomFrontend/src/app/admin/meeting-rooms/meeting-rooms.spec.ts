import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingRooms } from './meeting-rooms';

describe('MeetingRooms', () => {
  let component: MeetingRooms;
  let fixture: ComponentFixture<MeetingRooms>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeetingRooms]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MeetingRooms);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
