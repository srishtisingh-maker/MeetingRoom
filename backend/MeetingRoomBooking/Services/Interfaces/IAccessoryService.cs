using MeetingRoomBooking.DTOS.Accessory;

namespace MeetingRoomBooking.Services.Interfaces
{
    public interface IAccessoryService
    {
        Task<IEnumerable<AccessoryResponseDto>> GetAllAsync();
        Task<AccessoryResponseDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateAccessoryDto dto);
        Task<bool> UpdateAsync(int id, UpdateAccessoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
