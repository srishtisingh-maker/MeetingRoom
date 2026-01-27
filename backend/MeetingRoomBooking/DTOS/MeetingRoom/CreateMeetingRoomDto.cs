namespace MeetingRoomBooking.DTOS.MeetingRoom
{
    public class CreateMeetingRoomDto
    {
        public string Name { get; set; }

        public string Building { get; set; }

        public int Floor { get; set; }

        public int Capacity { get; set; }

        public List<int> AccessoryIds { get; set; }
    }
}
