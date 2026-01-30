using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.DTOS.User
{
    public class UpdateUserProfileDto
    {
 
        [MaxLength(100)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public IFormFile? ProfileImage { get; set; }
    }
}
