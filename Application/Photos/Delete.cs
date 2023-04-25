// <summary>
/// Class <c>Delete</c> deletes a photo based on the provided public ID and photo parameters.
/// </summary>
/// <remarks>
/// The class contains a nested <c>Command</c> class which specifies the properties required to execute the delete photo request.
/// The class also contains a nested <c>Handler</c> class which handles the delete photo request and updates the database accordingly.
/// </remarks>
using Application.Core;
using Application.Core.Parameters;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos;

public class Delete
{
    /// <summary>
    /// Class <c>Command</c> specifies the properties required to execute the delete photo request.
    /// </summary>
    public class Command : IRequest<ResultValidator<Unit>>
    {
        /// <summary>
        /// The public ID of the photo to be deleted.
        /// </summary>
        public string PublicId { get; set; }
        /// <summary>
        /// The photo parameters.
        /// </summary>
        public PhotoQuery Param { get; set; }
    }

    /// <summary>
    /// Class <c>Handler</c> handles the delete photo request and updates the database accordingly.
    /// </summary>
    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IPhotoAccessor _photoAccessor;

        public Handler(DataContext context, IPhotoAccessor photoAccessor)
        {
            _context = context;
            _photoAccessor = photoAccessor;
        }

        /// <summary>
        /// Handles the delete photo request and updates the database accordingly.
        /// </summary>
        /// <param name="request">The delete photo request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A result indicating whether the delete operation was successful.</returns>
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var photo = await _context.Photos.FindAsync(request.PublicId, cancellationToken);
            if (photo == null) return ResultValidator<Unit>.Error("Can't Find The Photo", 404);

            switch (request.Param.To)
            {
                case "director":
                    var director = await _context.Directors.FirstOrDefaultAsync(x => x.Id == int.Parse(request.Param.Id));
                    if (director == null) return ResultValidator<Unit>.Error("Can't Find The Director", 404);
                    director.Photo = null;
                    break;

                case "actor":
                    var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == int.Parse(request.Param.Id));
                    if (actor == null) return ResultValidator<Unit>.Error("Can't Find The Director", 404);
                    actor.Photo = null;
                    break;

                case "movie":
                    if (photo.IsMain) return ResultValidator<Unit>.Error("Can't Delete Main Photo", 400);
                    break;

                case "user":
                    if (photo.IsMain) return ResultValidator<Unit>.Error("Can't Delete Main Photo", 400);
                    break;

                default:
                    return ResultValidator<Unit>.Error("Pleae Provided Right Parameters", 400);
            }

            var resultDelete = await _photoAccessor.DeletePhoto(request.PublicId);
            if (resultDelete == null) return ResultValidator<Unit>.Error("Error While Saving Change In Cloud", 400);

            _context.Photos.Remove(photo);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0
                ? ResultValidator<Unit>.Success(Unit.Value, 200)
                : ResultValidator<Unit>.Error("Error While Saving Change In Database", 400);
        }
    }
}