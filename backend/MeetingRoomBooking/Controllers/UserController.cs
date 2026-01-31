using MeetingRoomBooking.DTOS.User;
using MeetingRoomBooking.Middleware;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.Services;
using MeetingRoomBooking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingRoomBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("by-email")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var user = await _service.GetByEmailAsync(email)
                ?? throw new AppException("User not found", StatusCodes.Status404NotFound);

            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await _service.CreateAsync(user);
            return Ok(createdUser);
        }

        [HttpPut("profile")]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateUserProfileDto dto)
        {
            Console.WriteLine(ClaimTypes.NameIdentifier);
            //var userIdClaim = User.FindFirst(int.Parse(dto.Id));
            //var userIdClaim = User.FindFirst(c => c.Value == dto.Id);


            //if (userIdClaim == null)
            //    throw new AppException("Invalid token", StatusCodes.Status401Unauthorized);

            //int userId = int.Parse(userIdClaim.Value);

            var user = await _service.UpdateProfileAsync(dto.Id, dto);

            return Ok(new
            {
                message = "Profile updated successfully",
                user
            });
        }


        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                throw new AppException("Invalid token", StatusCodes.Status401Unauthorized);

            var user = await _service.GetByEmailAsync(email)
                ?? throw new AppException("User not found", StatusCodes.Status404NotFound);

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Role,
                user.ProfileImageUrl,
                user.CreatedOn
            });
        }
    }
}
