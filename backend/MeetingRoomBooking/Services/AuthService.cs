using MeetingRoomBooking.DTOS.Auth;
using MeetingRoomBooking.Middleware;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.Repositories.Interfaces;
using MeetingRoomBooking.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeetingRoomBooking.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            dto.Email = dto.Email.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new AppException("Email is required");
            
            if (!IsValidEmail(dto.Email))
                throw new AppException("Invalid email format", 400);
            //check is email already exists
            if (await _userRepository.EmailExistsAsync(dto.Email))
                throw new AppException("Email already registered", 409);

            //check if password is valid
            var passwordError = ValidatePassword(dto.Password);
            if (passwordError != null)
                throw new AppException(passwordError, 400);

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = hashedPassword,
                Role = "Employee"
            };
            await _userRepository.AddAsync(user);
        }
        private bool IsValidEmail(string email)
        {
            return System.Net.Mail.MailAddress.TryCreate(email, out _);
        }

        private string? ValidatePassword(string password)
        {
            

            if (string.IsNullOrWhiteSpace(password))
                return "Password is required";

            if (password.Length < 6)
                return "Password should be at least 6 characters long";

            bool hasLetter = password.Any(char.IsLetter);
            bool hasDigit = password.Any(char.IsDigit);

            if (!hasLetter || !hasDigit)
                return "Password should have at least 1 digit and 1 alphabet";

            return null; // valid password
        }
        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return null;
            }
            var token = GenerateJwt(user);
            return new AuthResponseDto
            {
                Token = token,
                Role = user.Role
            };
        }
        private string GenerateJwt(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
       

    }
}
