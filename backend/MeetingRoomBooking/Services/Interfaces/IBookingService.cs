using MeetingRoomBooking.DTOS.Booking;

namespace MeetingRoomBooking.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingResponseDto>> GetAllAsync();               // Admin only
        Task<IEnumerable<BookingResponseDto>> GetByUserAsync(int userId); // Employee
        Task<BookingResponseDto> GetByIdAsync(int id);                    // Both roles
        Task CreateAsync(int userId, CreateBookingDto dto);               // Employee

        Task<bool> UpdateByAdminAsync(int id, int adminId, UpdateBookingByAdminDto dto);
        Task<bool> UpdateByEmployeeAsync(int id, int userId, UpdateBookingByEmployeeDto dto); // Employee can update own
        Task<bool> CancelAsync(int id, int userId);                        // Employee cancel
        Task<bool> ApproveAsync(int id, int adminId);                     // Admin approve
        Task<bool> RejectAsync(int id, int adminId);                      // Admin reject
    }
}

