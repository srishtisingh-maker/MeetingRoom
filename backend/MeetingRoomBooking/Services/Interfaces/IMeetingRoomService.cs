using MeetingRoomBooking.DTOS.MeetingRoom;

namespace MeetingRoomBooking.Services.Interfaces
{
    public interface IMeetingRoomService
    {
        Task<List<MeetingRoomResponseDto>> GetAllAsync();

        Task<List<MeetingRoomResponseDto>> GetActiveAsync();

        Task<MeetingRoomResponseDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateMeetingRoomDto dto);
        Task<bool> UpdateAsync(int id, UpdateMeetingRoomDto dto);
        Task<bool> DeleteAsync(int id); 
    }
}
