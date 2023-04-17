using API.Controllers.DTO;
using Application.Interface;
using Application.Photos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace API.Controllers;

public class PhotosController : BaseApiController
{
    private readonly DataContext _context;
    private readonly IPhotoAccessor _photoAccessor;
    private readonly IUserAccessor _userAccessor;

    public PhotosController(
        DataContext context,
        IPhotoAccessor photoAccessor,
        IUserAccessor userAccessor)
    {
        _context = context;
        _photoAccessor = photoAccessor;
        _userAccessor = userAccessor;
    }

    [HttpPost("profile-upload")]
    public async Task<IActionResult> PostPhotoProfile([FromForm] PhotoUploadModel file)
    {
        var user = await _context.Users
            .Include(x => x.Photos)
            .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

        if (user == null) return NotFound(CreateResponse(StatusCodes.Status404NotFound, "Can't Find User"));

        var uploadResult = await _photoAccessor.UserProfileUploadPhoto500X500(file.File);

        if (uploadResult == null)
            return NotFound(CreateResponse(StatusCodes.Status404NotFound, "Please Provide A Photo File"));

        var photo = new Photo()
        {
            Id = uploadResult.PublicId,
            Url = uploadResult.Url,
        };

        if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;

        user.Photos.Add(photo);

        var result = await _context.SaveChangesAsync() > 0;

        return result
            ? Ok(CreateResponse(StatusCodes.Status200OK, "Successfully Adding Photo"))
            : BadRequest(CreateResponse(StatusCodes.Status400BadRequest, "There Is Something Wrong While Saving Photo In Database"));
    }

    [HttpPost("{id}/movie-upload/landscape")]
    public async Task<IActionResult> PostPhotoMovieLandscape([FromForm] PhotoUploadModel file, [FromRoute] Guid id)
    {
        var movie = await _context.Movies
            .Include(x => x.Photos)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (movie == null) return NotFound(CreateResponse(StatusCodes.Status404NotFound, "Can't Find Movie"));
    
        var cloudinaryResult = await _photoAccessor.MovieUploadPhotoLandscape(file.File);
        if (cloudinaryResult == null) return NotFound(CreateResponse(StatusCodes.Status404NotFound, "Please Provide Photo File"));

        var photo = new Photo()
        {
            Id = cloudinaryResult.PublicId,
            Url = cloudinaryResult.Url,
        };

        if (!movie.Photos.Any(x => x.IsMain)) photo.IsMain = true;
        
        movie.Photos.Add(photo);

        var result = await _context.SaveChangesAsync() > 0;

        return result
            ? Ok(CreateResponse(StatusCodes.Status200OK, "Successfully Adding Photo"))
            : BadRequest(CreateResponse(StatusCodes.Status400BadRequest, "There Is Something Wrong While Savin"));
    }
    
    [HttpPost("{id}/movie-upload/portrait")]
    public async Task<IActionResult> PostPhotoMoviePortrait([FromForm] PhotoUploadModel file, [FromRoute] Guid id)
    {
        var movie = await _context.Movies
            .Include(x => x.Photos)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (movie == null) return NotFound(CreateResponse(StatusCodes.Status404NotFound, "Can't Find Movie"));
    
        var cloudinaryResult = await _photoAccessor.MovieUploadPhotoPortrait(file.File);
        if (cloudinaryResult == null) return NotFound(CreateResponse(StatusCodes.Status404NotFound, "Please Provide Photo File"));

        var photo = new Photo()
        {
            Id = cloudinaryResult.PublicId,
            Url = cloudinaryResult.Url,
        };

        if (!movie.Photos.Any(x => x.IsMain)) photo.IsMain = true;
        
        movie.Photos.Add(photo);

        var result = await _context.SaveChangesAsync() > 0;

        return result
            ? Ok(CreateResponse(StatusCodes.Status200OK, "Successfully Adding Photo"))
            : BadRequest(CreateResponse(StatusCodes.Status400BadRequest, "There Is Something Wrong While Savin"));
    }

    [HttpDelete("{publicId}")]
    public async Task<IActionResult> DeletePhoto([FromRoute] string publicId)
    {
        var result = await Mediator.Send(new ProfileDelete.Command { PublicId = publicId });

        return GetResult(result);
    }

    [HttpPut("{publicId}/profile-manage-main")]
    public async Task<IActionResult> SetMainProfile(string publicId)
    {
        var result = await Mediator.Send(new ProfileMain.Command { PublicId = publicId });

        return GetResult(result);
    }
}