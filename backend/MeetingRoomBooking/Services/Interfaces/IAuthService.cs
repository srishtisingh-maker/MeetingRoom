using MeetingRoomBooking.DTOS.Auth;

namespace MeetingRoomBooking.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
