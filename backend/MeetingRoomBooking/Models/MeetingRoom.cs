using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Models
{
    public class MeetingRoom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Building { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int Capacity { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public ICollection<RoomAccessory> RoomAccessories { get; set; }

    }
}
