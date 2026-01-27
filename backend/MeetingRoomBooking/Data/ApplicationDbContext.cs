using MeetingRoomBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Accessory> Accessories { get; set; } 
        public DbSet<RoomAccessory> RoomAccessories { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //unique meeting room name
            modelBuilder.Entity<MeetingRoom>()
                .HasIndex(r => r.Name)
                .IsUnique();

            //unique accessory name
            modelBuilder.Entity<Accessory>()
                .HasIndex(a => a.Name)
                .IsUnique();

            //composite PK for RoomAccessory
            modelBuilder.Entity<RoomAccessory>()
                .HasKey(ra => new { ra.RoomId, ra.AccessoryId });

            modelBuilder.Entity<RoomAccessory>()
                .HasOne(ra => ra.MeetingRoom)
                .WithMany(r => r.RoomAccessories)
                .HasForeignKey(ra => ra.RoomId);

            modelBuilder.Entity<RoomAccessory>()
                .HasOne(ra=> ra.Accessory)
                .WithMany(a => a.RoomAccessories)
                .HasForeignKey(ra => ra.AccessoryId);

            //User should have unique email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Accessory>()
            .Property(a => a.IsActive)
            .HasDefaultValue(true);

            modelBuilder.Entity<User>()
            .Property(u => u.IsActive)
            .HasDefaultValue(true);

            modelBuilder.Entity<MeetingRoom>()
            .Property(r => r.IsActive)
            .HasDefaultValue(true);

        }
    }
}
