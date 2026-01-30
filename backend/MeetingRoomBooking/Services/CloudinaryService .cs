using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MeetingRoomBooking.Services.Interfaces;

namespace MeetingRoomBooking.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration config)
        {
            var settings = config.GetSection("Cloudinary");
            var account = new Account(
                settings["CloudName"],
                settings["ApiKey"],
                settings["ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string?> UploadProfileImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = "employee-profiles",
                Transformation = new Transformation()
                    .Width(300)
                    .Height(300)
                    .Crop("fill")
                    .Gravity("face")
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            return result.SecureUrl?.ToString();
        }
    }
}