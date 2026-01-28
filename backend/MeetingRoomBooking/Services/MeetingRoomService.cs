using MeetingRoomBooking.DTOS.MeetingRoom;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.Repositories.Interfaces;
using MeetingRoomBooking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace MeetingRoomBooking.Services
{
    public class MeetingRoomService : IMeetingRoomService
    {
        private readonly IMeetingRoomRepository _repository;

        public MeetingRoomService(IMeetingRoomRepository repository)
        {
            _repository = repository;
        }
        [Authorize]
        public async Task<List<MeetingRoomResponseDto>> GetAllAsync()
        {
            var rooms = await _repository.GetAllAsync();
            return rooms
                .Select(r => new MeetingRoomResponseDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Building = r.Building,
                    Floor = r.Floor,
                    Capacity = r.Capacity,
                    IsActive = r.IsActive,
                    Accessories = r.RoomAccessories
                        .Select(ra => ra.Accessory.Name)
                        .ToList()
                })
                .ToList();
        }
        [Authorize]
        public async Task<List<MeetingRoomResponseDto>> GetActiveAsync()
        {
            var rooms = await _repository.GetActiveAsync();

            return rooms.Select(r => new MeetingRoomResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Building = r.Building,
                Floor = r.Floor,
                Capacity = r.Capacity,
                IsActive = r.IsActive,
                Accessories = r.RoomAccessories
                    .Select(ra => ra.Accessory.Name)
                    .ToList()
            }).ToList();
        }

        [Authorize]
        public async Task<MeetingRoomResponseDto?> GetByIdAsync(int id)
        {
            var room = await _repository.GetByIdAsync(id);
            if (room == null)
                return null;
            return new MeetingRoomResponseDto
            {
                Id = room.Id,
                Name = room.Name,
                Building = room.Building,
                Floor = room.Floor,
                Capacity = room.Capacity,
                IsActive = room.IsActive,
                Accessories = room.RoomAccessories
                    .Select(ra => ra.Accessory.Name)
                    .ToList()
            };
        }
        [Authorize(Roles = "Admin")]
        public async Task CreateAsync(CreateMeetingRoomDto dto)
        {
            var room = new MeetingRoom
            {
                Name = dto.Name,
                Building = dto.Building,
                Floor = dto.Floor,
                Capacity = dto.Capacity,
                IsActive = true
            };
            await _repository.AddAsync(room);
            await _repository.SaveAsync();
        }
        [Authorize(Roles = "Admin")]
        public async Task<bool> UpdateAsync(int id, UpdateMeetingRoomDto dto)
        {
            var room = await _repository.GetByIdAsync(id);
            if (room == null)
                return false;
            room.Name = dto.Name;
            room.Building = dto.Building;
            room.Floor = dto.Floor;
            room.Capacity = dto.Capacity;
            room.IsActive = dto.IsActive;
            room.RoomAccessories.Clear();

            // Add new accessories from dto
            foreach (var accId in dto.AccessoryIds)
            {
                room.RoomAccessories.Add(new RoomAccessory
                {
                    AccessoryId = accId,
                    RoomId = room.Id
                });
            }
            await _repository.UpdateAsync(room);
            await _repository.SaveAsync();
            return true;
        }
        [Authorize(Roles = "Admin")]
        public async Task<bool> DeleteAsync(int id)
        {
            var room = await _repository.GetByIdAsync(id);
            if (room == null)
                return false;
            room.IsActive = false;//soft delete
            await _repository.UpdateAsync(room);
            await _repository.SaveAsync();
            return true;
        }
    }
}

