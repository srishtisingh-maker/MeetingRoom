using MeetingRoomBooking.DTOS.Booking;
using MeetingRoomBooking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingRoomBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;
        public BookingController(IBookingService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        // GET: /api/Booking
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());


        // GET: /api/booking/my
        [Authorize(Roles = "Employee")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyBookings()
        => Ok(await _service.GetByUserAsync(GetUserId()));

        // GET: /api/booking/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _service.GetByIdAsync(id);
            return booking == null ? NotFound() : Ok(booking);
        }

        [Authorize]
        // POST: /api/booking
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingDto dto)
        {
            await _service.CreateAsync(GetUserId(), dto);
            return Ok(new { message = "Meeting room created successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("admin/{id}")]
        public async Task<IActionResult> UpdateByAdmin(int id, UpdateBookingByAdminDto dto)
        {
            var result = await _service.UpdateByAdminAsync(id, GetUserId(), dto);
            return result ? Ok("Booking updated by admin") : BadRequest("Cannot update booking");
        }

        // PUT: /api/booking/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByEmployee(int id, UpdateBookingByEmployeeDto dto)
        {
            var result = await _service.UpdateByEmployeeAsync(id, GetUserId(), dto);
            return result ? Ok("Booking updated") : BadRequest("Cannot update");
        }


        // DELETE: /api/booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var result = await _service.CancelAsync(id, GetUserId());
            return result ? Ok("Booking cancelled") : BadRequest("Cannot cancel");
        }

        // Admin Approve
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            var result = await _service.ApproveAsync(id, GetUserId());
            return result ? Ok("Booking approved") : BadRequest("Cannot approve");
        }

        // Admin Reject
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(int id)
        {
            var result = await _service.RejectAsync(id, GetUserId());
            return result ? Ok("Booking rejected") : BadRequest("Cannot reject");
        }
    }
}
