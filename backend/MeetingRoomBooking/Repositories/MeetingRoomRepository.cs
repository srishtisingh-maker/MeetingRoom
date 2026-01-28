using MeetingRoomBooking.Data;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Repositories
{
    public class MeetingRoomRepository : IMeetingRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public MeetingRoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<MeetingRoom>> GetAllAsync()
        {
            return await _context.MeetingRooms
                .Include(r => r.RoomAccessories)
                .ThenInclude(ra => ra.Accessory)
                .ToListAsync();
        }
        public async Task<List<MeetingRoom>> GetActiveAsync()
        {
            return await _context.MeetingRooms
                .Where(r => r.IsActive)
                .Include(r => r.RoomAccessories)
                .ThenInclude(ra => ra.Accessory)
                .ToListAsync();
        }

        public async Task<MeetingRoom?> GetByIdAsync(int id)
        {
            return await _context.MeetingRooms
                .Include(r => r.RoomAccessories)
                .ThenInclude(ra => ra.Accessory)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task AddAsync(MeetingRoom room)
        {
            await _context.MeetingRooms.AddAsync(room);
        }
        public Task UpdateAsync(MeetingRoom room)
        {
            _context.MeetingRooms.Update(room);
            return Task.CompletedTask;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
