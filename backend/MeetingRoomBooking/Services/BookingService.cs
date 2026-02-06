using MeetingRoomBooking.DTOS.Booking;
using MeetingRoomBooking.Middleware;
using MeetingRoomBooking.Repositories.Interfaces;
using MeetingRoomBooking.Services.Interfaces;

namespace MeetingRoomBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly IMeetingRoomRepository _roomRepository;

        public BookingService(IBookingRepository repository, IMeetingRoomRepository roomRepository)
        {
            _repository = repository;
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<BookingResponseDto>> GetAllAsync()
        {
            var bookings = await _repository.GetAllAsync();
            return bookings.Select(MapToDto);
        }

        public async Task<IEnumerable<BookingResponseDto>> GetByUserAsync(int userId)
        {
            var bookings = await _repository.GetByUserIdAsync(userId);
            return bookings.Select(MapToDto);
        }

        public async Task<BookingResponseDto> GetByIdAsync(int id)
        {
            var b = await _repository.GetByIdAsync(id);
            return b == null ? null : MapToDto(b);
        }
        //Changed
        public async Task CreateAsync(int userId, CreateBookingDto dto)
        {
            var room = await _roomRepository.GetByIdAsync(dto.RoomId);
            if (room == null || !room.IsActive)
                throw new AppException("Invalid room");

            var isAvailable = await _repository.IsRoomAvailableAsync(
                dto.RoomId,
                dto.Date,
                dto.StartTime,
                dto.EndTime
            );

            if (!isAvailable)
                throw new AppException("Room is not available for selected time");

            var booking = new Booking
            {
                UserId = userId,
                RoomId = dto.RoomId,
                Date = dto.Date.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Participants = dto.Participants,
                RequestedOn = DateTime.UtcNow
            };

            await _repository.CreateAsync(booking);
        }
        
        public async Task<bool> UpdateByAdminAsync(int id, int adminId, UpdateBookingByAdminDto dto)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                return false;

            booking.RoomId = dto.RoomId;
            booking.Date = dto.Date;
            booking.StartTime = dto.StartTime;
            booking.EndTime = dto.EndTime;
            booking.Participants = dto.Participants;
            booking.Status = dto.Status;

            booking.ActionBy = adminId;
            booking.ActionOn = DateTime.Now;

            await _repository.UpdateAsync(booking);
            return true;
        }

        public async Task<bool> UpdateByEmployeeAsync(int id, int userId, UpdateBookingByEmployeeDto dto)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null || booking.UserId != userId)
                return false;

            booking.Date = dto.Date;
            booking.StartTime = dto.StartTime;
            booking.EndTime = dto.EndTime;
            booking.Participants = dto.Participants;

            await _repository.UpdateAsync(booking);
            return true;
        }
      
        public async Task<bool> CancelAsync(int id, int userId)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null || booking.UserId != userId)
                return false;

            booking.Status = "Cancelled";
            await _repository.UpdateAsync(booking);
            return true;
        }
        //changed
        public async Task<bool> ApproveAsync(int id, int adminId)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null || booking.Status != "Pending")
                return false;

            var isAvailable = await _repository.IsRoomAvailableAsync(
                booking.RoomId,
                booking.Date,
                booking.StartTime,
                booking.EndTime,
                booking.Id
            );

            if (!isAvailable)
                throw new AppException("Room already booked for this time");

            booking.Status = "Approved";
            booking.ActionBy = adminId;
            booking.ActionOn = DateTime.UtcNow;

            await _repository.UpdateAsync(booking);
            return true;
        }


        public async Task<bool> RejectAsync(int id, int adminId)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null || booking.Status != "Pending")
                return false;

            booking.Status = "Rejected";
            booking.ActionBy = adminId;
            booking.ActionOn = DateTime.UtcNow;
            await _repository.UpdateAsync(booking);
            return true;
        }

        private BookingResponseDto MapToDto(Booking b)
        {
            return new BookingResponseDto
            {
                Id = b.Id,
                RoomId = b.RoomId,
                RoomName = b.MeetingRoom?.Name,
                UserId = b.UserId,
                UserName = b.User?.Name,
                Date = b.Date,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                Participants = b.Participants,
                Status = b.Status,
                RequestedOn = b.RequestedOn,
                ActionBy = b.ActionBy,
                ActionOn = b.ActionOn
            };
        }
    }
}
