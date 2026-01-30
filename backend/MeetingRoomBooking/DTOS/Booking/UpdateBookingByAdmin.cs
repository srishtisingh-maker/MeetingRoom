namespace MeetingRoomBooking.DTOS.Booking
{
    public class UpdateBookingByAdminDto
    {
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int Participants { get; set; }
        public string Status { get; set; }   // Approved / Rejected / Cancelled
    }

}
