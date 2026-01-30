using MeetingRoomBooking.Data;
using MeetingRoomBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
              .Include(b => b.User)
              .Include(b => b.MeetingRoom)
              .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByUserIdAsync(int userId)
        {
            return await _context.Bookings
              .Where(b => b.UserId == userId)
              .Include(b => b.MeetingRoom)
              .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
              .Include(b => b.User)
              .Include(b => b.MeetingRoom)
              .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task CreateAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
