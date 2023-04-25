using Application.Core;
using Application.Interface;
using MediatR;

namespace Application.Photos;
/// <summary>
/// Class <c>Upload</c> is responsible for handling the logic of uploading a photo.
/// </summary>
public class Upload
{
    /// <summary>
    /// Class <c>Command</c> defines the input parameters for uploading a photo.
    /// </summary>
    public class Command : IRequest<ResultValidator<PhotoResponder>>
    {
        /// <summary>
        /// The photo file stream to be uploaded.
        /// </summary>
        public Stream File { get; set; }
        /// <summary>
        /// The name of the photo file to be uploaded.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// The parameter for uploading the photo in square, landscape, or portrait mode.
        /// </summary>
        public string Param { get; set; }
    }

    /// <summary>
    /// Class <c>Handler</c> handles the logic of uploading a photo.
    /// </summary>
    public class Handler : IRequestHandler<Command, ResultValidator<PhotoResponder>>
    {
        private readonly IPhotoAccessor _photoAccessor;

        /// <summary>
        /// Constructor for the <c>Handler</c> class.
        /// </summary>
        /// <param name="photoAccessor">An instance of the photo accessor interface.</param>
        public Handler(IPhotoAccessor photoAccessor)
        {
            _photoAccessor = photoAccessor;
        }

        /// <summary>
        /// Handles the logic of uploading a photo.
        /// </summary>
        /// <param name="request">The input parameters for uploading a photo.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A result validator containing the response from the photo accessor.</returns>
        public async Task<ResultValidator<PhotoResponder>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                PhotoResponder uploadResult = null;

                // Upload the photo based on the specified parameter.
                switch (request.Param)
                {
                    case "square":
                        uploadResult = await _photoAccessor.UserProfileUploadPhotoSquare(request.FileName, request.File);
                        break;
                        
                    case "landscape":
                        uploadResult = await _photoAccessor.MovieUploadPhotoLandscape(request.FileName, request.File);
                        break;

                    case "portrait":
                        uploadResult = await _photoAccessor.MovieUploadPhotoPortrait(request.FileName, request.File);
                        break;

                    default:
                        return ResultValidator<PhotoResponder>.Error("Pleae Provided Right Parameters", 400);
                }

                // Return a success result validator if the photo was uploaded successfully,
                // otherwise return an error result validator.
                return uploadResult != null ?
                    ResultValidator<PhotoResponder>.Success(uploadResult, 200)
                    : ResultValidator<PhotoResponder>.Error("Please provide the correct parameters.", 400);
            }
            catch (Exception)
            {
                // Return an error result validator if an exception occurs during the upload.
                return ResultValidator<PhotoResponder>.Error("Error while saving the image in cloud.", 400);
            }
        }
    }
}