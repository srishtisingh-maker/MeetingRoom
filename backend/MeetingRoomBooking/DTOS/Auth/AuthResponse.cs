using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.DTOS.Auth
{
    public class AuthResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }


        public string Role { get; set; }

        public string? ProfileImageUrl { get; set; }

        public string Token { get; set; }
    }

}
