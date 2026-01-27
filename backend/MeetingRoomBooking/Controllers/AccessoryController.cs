using MeetingRoomBooking.DTOS.Accessory;
using MeetingRoomBooking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoryController : ControllerBase
    {
        private readonly IAccessoryService _service;

        public AccessoryController(IAccessoryService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var accessory = await _service.GetByIdAsync(id);
            return accessory == null ? NotFound("Accessory not found") : Ok(accessory);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccessoryDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok("Accessory created");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? Ok("Accessory deleted") : NotFound("Accessory not found");
        }
    }
}
