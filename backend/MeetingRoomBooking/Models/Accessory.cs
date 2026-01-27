using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Models
{
    public class Accessory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<RoomAccessory> RoomAccessories { get; set; }

    }
}
