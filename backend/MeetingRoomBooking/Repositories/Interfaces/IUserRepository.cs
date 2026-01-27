using MeetingRoomBooking.Models;

namespace MeetingRoomBooking.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        Task AddAsync(User user);
    }
}
