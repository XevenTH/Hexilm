using Application.Interface;
using Application.Photos;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace SecurityInfrastructure.CloudinarySecurity;

public class PhotoAccessor : IPhotoAccessor
{
    private readonly Cloudinary _cloudinary;

    public PhotoAccessor(IOptions<CloudinaryConfiguration> configuration)
    {
        var account = new Account(
            configuration.Value.CloudName,
            configuration.Value.ApiKey,
            configuration.Value.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<PhotoResult> UserProfileUploadPhoto500X500(IFormFile file)
    {
        if (file.Length <= 0) return null;
        
        await using var readFile = file.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.Name, readFile),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill"),
            Format = "webp"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error != null) throw new Exception(result.Error.Message);

        return new PhotoResult()
        {
            PublicId = result.PublicId,
            Url = result.SecureUrl.ToString()
        };
    }

    public async Task<PhotoResult> MovieUploadPhotoLandscape(IFormFile file)
    {
        if (file.Length <= 0) return null;
        
        await using var readFile = file.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.Name, readFile),
            Transformation = new Transformation().Height(1080).Width(1440).Crop("fill"),
            Format = "webp"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null) throw new Exception(uploadResult.Error.Message);

        return new PhotoResult()
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url.ToString()
        };
    }

    public async Task<PhotoResult> MovieUploadPhotoPortrait(IFormFile file)
    {
        if (file.Length <= 0) return null;
        
        await using var readFile = file.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.Name, readFile),
            Transformation = new Transformation().Height(1440).Width(1080).Crop("fill"),
            Format = "webp"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null) throw new Exception(uploadResult.Error.Message);

        return new PhotoResult()
        {
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url.ToString()
        };
    }

    public async Task<string> DeletePhoto(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deletionParams);
        return result.Result == "ok" ? result.Result : null;
    }
}