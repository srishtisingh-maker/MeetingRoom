using MeetingRoomBooking.DTOS.Accessory;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.Repositories.Interfaces;
using MeetingRoomBooking.Services.Interfaces;
using Microsoft.VisualBasic;

namespace MeetingRoomBooking.Services
{
    public class AccessoryService : IAccessoryService
    {
        private readonly IAccessoryRepository _repository;

        public AccessoryService(IAccessoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AccessoryResponseDto>> GetAllAsync()
        {
            var accessories = await _repository.GetAllAsync();
            return accessories.Select(a => new AccessoryResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                IsActive = a.IsActive
            });
        }
        public async Task<AccessoryResponseDto?> GetByIdAsync(int id)
        {
            var accessory = await _repository.GetByIdAsync(id);
            if (accessory == null) return null;
            return new AccessoryResponseDto
            {
                Id = accessory.Id,
                Name = accessory.Name,
                IsActive = accessory.IsActive
            };
        }

        public async Task CreateAsync(CreateAccessoryDto dto)
        {
            var accessory = new Accessory
            {
                Name = dto.Name,
                IsActive = true
            };
            await _repository.CreateAsync(accessory);
        }

        public async Task<bool> UpdateAsync(int id, UpdateAccessoryDto dto)
        {
            var accessory = await _repository.GetByIdAsync(id);
            if (accessory == null) return false;

            accessory.Name = dto.Name;
            accessory.IsActive = dto.IsActive;

            await _repository.UpdateAsync(accessory);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var accessory = await _repository.GetByIdAsync(id);
            if (accessory == null) return false;
            await _repository.DeleteAsync(accessory);
            return true;
        }






    }
}
