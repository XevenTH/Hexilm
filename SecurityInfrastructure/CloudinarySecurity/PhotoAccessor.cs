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

    public async Task<PhotoResult> UploadPhoto(IFormFile file)
    {
        if (file.Length > 0)
        {
            await using var readFile = file.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.Name, readFile),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill")
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null) throw new Exception(result.Error.Message);

            return new PhotoResult()
            {
                PublicId = result.PublicId,
                Url = result.SecureUrl.ToString()
            };
        }

        return null;
    }

    public async Task<string> DeletePhoto(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deletionParams);
        return result.Result == "ok" ? result.Result : null;
    }
}