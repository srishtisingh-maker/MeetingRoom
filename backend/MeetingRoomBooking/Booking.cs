using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MeetingRoomBooking.Models;
namespace MeetingRoomBooking
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("MeetingRoom")]
        public int RoomId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public int Participants { get; set; }

        public string status { get; set; } = "Pending";

        public DateTime RequestedOn { get; set; } = DateTime.Now;

        public int? ActionBy { get; set; }

        public DateTime? ActionOn { get; set; }

        public User User { get; set; }

        public MeetingRoom MeetingRoom { get; set; }

    }
}
