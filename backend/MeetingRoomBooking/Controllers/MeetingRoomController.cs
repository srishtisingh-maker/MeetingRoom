using MeetingRoomBooking.DTOS.MeetingRoom;
using MeetingRoomBooking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingRoomController : ControllerBase
    {
        private readonly IMeetingRoomService _service;
        public MeetingRoomController(IMeetingRoomService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [Authorize]
        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            return Ok(await _service.GetActiveAsync());
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _service.GetByIdAsync(id);
            return room == null ? NotFound() : Ok(room);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMeetingRoomDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok("Meetig room created");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMeetingRoomDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result ?  Ok(new { message = "Meeting room updated successfully" }) : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? Ok(new { message = "Room deactivated" }) : NotFound();
        }
    }
}
