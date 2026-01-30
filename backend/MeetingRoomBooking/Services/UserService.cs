using MeetingRoomBooking.DTOS.User;
using MeetingRoomBooking.Middleware;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.Repositories.Interfaces;
using MeetingRoomBooking.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace MeetingRoomBooking.Services
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository _userRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public UserService(
        IUserRepository userRepository,
        ICloudinaryService cloudinaryService)
        {
            _userRepository = userRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<User> CreateAsync(User user)
        {
            if (await _userRepository.EmailExistsAsync(user.Email))
                throw new Exception("Email already exists");

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<UserResponseDto> UpdateProfileAsync(int userId, UpdateUserProfileDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new AppException("User not found", StatusCodes.Status404NotFound);

            if (!string.IsNullOrWhiteSpace(dto.Name))
                user.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                if (await _userRepository.EmailExistsAsync(dto.Email))
                    throw new AppException("Email already exists", StatusCodes.Status400BadRequest);

                user.Email = dto.Email;
            }

            if (dto.ProfileImage != null)
            {
                if (!dto.ProfileImage.ContentType.StartsWith("image/"))
                    throw new AppException("Invalid image format");

                if (dto.ProfileImage.Length > 5 * 1024 * 1024)
                    throw new AppException("Image size exceeds 5MB");

                user.ProfileImageUrl =
                    await _cloudinaryService.UploadProfileImageAsync(dto.ProfileImage);
            }

            await _userRepository.UpdateAsync(user);

            // Map User to UserResponseDto
            var userResponseDto = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl
            };

            return userResponseDto;
        }




    }
}
