using MeetingRoomBooking.Models;

namespace MeetingRoomBooking.Repositories.Interfaces
{
    public interface IAccessoryRepository
    {
        Task<List<Accessory>> GetAllAsync();
        Task<Accessory?> GetByIdAsync(int id);
        Task CreateAsync(Accessory accessory);
        Task UpdateAsync(Accessory accessory);
        Task DeleteAsync(Accessory accessory);
    }
}
