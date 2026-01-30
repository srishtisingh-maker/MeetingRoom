namespace MeetingRoomBooking.Services.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string?> UploadProfileImageAsync(IFormFile file);
    }
}
