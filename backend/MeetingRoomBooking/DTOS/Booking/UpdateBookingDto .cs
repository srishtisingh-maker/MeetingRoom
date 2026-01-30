namespace MeetingRoomBooking.DTOS.Booking
{
    public class UpdateBookingByEmployeeDto
    {
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int Participants { get; set; }
    }
}
