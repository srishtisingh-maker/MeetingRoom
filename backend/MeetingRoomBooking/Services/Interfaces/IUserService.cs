using MeetingRoomBooking.DTOS.User;
using MeetingRoomBooking.Models;

namespace MeetingRoomBooking.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> UpdateProfileAsync(int userId, UpdateUserProfileDto dto);


        Task<User> CreateAsync(User user);
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetByEmailAsync(string email);
    }
}
