namespace MeetingRoomBooking.Models
{
    public class RoomAccessory
    {
        //junction table for relation between MeetingRoom and Accessory
        public int RoomId { get; set; }
        public MeetingRoom MeetingRoom { get; set; }

        public int AccessoryId { get; set; }
        public Accessory Accessory { get; set; }
    }
}
