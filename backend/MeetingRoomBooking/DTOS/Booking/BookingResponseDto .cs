namespace MeetingRoomBooking.DTOS.Booking
{
    public class BookingResponseDto
    {
            public int Id { get; set; }
            public int RoomId { get; set; }
            public string RoomName { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }
            public DateTime Date { get; set; }
            public TimeOnly StartTime { get; set; }
            public TimeOnly EndTime { get; set; }
            public int Participants { get; set; }
            public string Status { get; set; }
            public DateTime RequestedOn { get; set; }
            public int? ActionBy { get; set; }
            public DateTime? ActionOn { get; set; }
     
    }
}
