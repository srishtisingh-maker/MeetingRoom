using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.DTOS.User
{
    public class UpdateUserProfileDto
    {
        public int Id { get; set; }
 
        [MaxLength(100)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public IFormFile? ProfileImage { get; set; }
    }
}
