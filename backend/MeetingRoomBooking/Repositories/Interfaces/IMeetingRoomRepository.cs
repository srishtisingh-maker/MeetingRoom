using MeetingRoomBooking.Models;

namespace MeetingRoomBooking.Repositories.Interfaces
{
    public interface IMeetingRoomRepository
    {
        Task<List<MeetingRoom>> GetAllAsync();
        Task<MeetingRoom?> GetByIdAsync(int id);
        Task AddAsync(MeetingRoom room);
        Task UpdateAsync(MeetingRoom room);
        Task SaveAsync();
    }
}
