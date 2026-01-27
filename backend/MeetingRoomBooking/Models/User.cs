using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

    

    }
}
