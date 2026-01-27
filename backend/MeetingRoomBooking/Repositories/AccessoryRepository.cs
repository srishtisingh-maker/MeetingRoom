using MeetingRoomBooking.Data;
using MeetingRoomBooking.Models;
using Microsoft.EntityFrameworkCore;
using MeetingRoomBooking.Repositories.Interfaces;

namespace MeetingRoomBooking.Repositories
{
    public class AccessoryRepository : IAccessoryRepository
    {
        private readonly ApplicationDbContext _context;

        public AccessoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Accessory>> GetAllAsync()
        {
            return await _context.Accessories.ToListAsync();
        }

        public async Task<Accessory?> GetByIdAsync(int id)
        {
            return await _context.Accessories.FindAsync(id);
        }

        public async Task CreateAsync(Accessory accessory)
        {
            _context.Accessories.Add(accessory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Accessory accessory)
        {
            _context.Accessories.Update(accessory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Accessory accessory)
        {
            _context.Accessories.Remove(accessory);
            await _context.SaveChangesAsync();
        }


    }
}
