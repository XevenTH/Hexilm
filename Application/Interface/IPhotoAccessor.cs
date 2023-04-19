using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Application.Interface;

public interface IPhotoAccessor
{
    Task<PhotoResult> UserProfileUploadPhoto500X500(IFormFile file);
    Task<PhotoResult> MovieUploadPhotoLandscape(IFormFile file);
    Task<PhotoResult> MovieUploadPhotoPortrait(IFormFile file);
    Task<string> DeletePhoto(string publicId);
}