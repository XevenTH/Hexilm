using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Application.Interface;

public interface IPhotoAccessor
{
    Task<PhotoResult> UploadPhoto(IFormFile file);
    Task<string> DeletePhoto(string publicId);
}