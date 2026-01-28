import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditMeetingRoom } from './edit-meeting-room';

describe('EditMeetingRoom', () => {
  let component: EditMeetingRoom;
  let fixture: ComponentFixture<EditMeetingRoom>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditMeetingRoom]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditMeetingRoom);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
