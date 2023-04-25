using Application.Photos;
namespace Application.Interface;

public interface IPhotoAccessor
{
    Task<PhotoResponder> UserProfileUploadPhotoSquare(string fileName, Stream file);
    Task<PhotoResponder> MovieUploadPhotoLandscape(string fileName, Stream file);
    Task<PhotoResponder> MovieUploadPhotoPortrait(string fileName, Stream file);
    Task<string> DeletePhoto(string publicId);
}