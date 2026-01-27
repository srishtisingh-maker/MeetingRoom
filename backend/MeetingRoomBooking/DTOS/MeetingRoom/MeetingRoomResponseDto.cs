namespace MeetingRoomBooking.DTOS.MeetingRoom
{
    public class MeetingRoomResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Building { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public List<string> Accessories { get; set; }
    }
}
