namespace MeetingRoomBooking.DTOS.User
{
    public class GetAllUsersResponseDto
    {
        public int Id { get; set; }          
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }

}
