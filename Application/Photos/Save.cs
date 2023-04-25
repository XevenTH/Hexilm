// <summary>
/// Class <c>Save</c> is responsible for handling the command to save a new photo to the database.
/// </summary>
namespace Application.Photos;

using Application.Core;
using Application.Core.Parameters;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

public class Save
{
    /// <summary>
    /// Class <c>Command</c> represents the request to save a new photo.
    /// </summary>
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public PhotoResponder photoResponder { get; set; }
        public PhotoQuery Param { get; set; }
    }
    /// <summary>
    /// Class <c>Handler</c> is responsible for handling the request to save a new photo.
    /// </summary>
    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Method <c>Handle</c> is responsible for handling the request to save a new photo.
        /// </summary>
        /// <param name="request">The command to save a new photo.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the result of the asynchronous operation.</returns>
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var newPhoto = new Photo
            {
                Id = request.photoResponder.PublicId,
                Url = request.photoResponder.Url,
                IsMain = true
            };

            switch (request.Param.To)
            {
                case "user":
                    var user = await _context.Users
                        .Include(x => x.Photos)
                        .FirstOrDefaultAsync(x => x.Id == _userAccessor.GetId(), cancellationToken);
                    if (user.Photos.Any()) newPhoto.IsMain = false;
                    user.Photos.Add(newPhoto);
                    break;

                case "actor":
                    var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == int.Parse(request.Param.Id), cancellationToken);
                    if (actor.Photo == null) actor.Photo = newPhoto;
                    break;

                case "director":
                    var director = await _context.Directors.FirstOrDefaultAsync(x => x.Id == int.Parse(request.Param.Id), cancellationToken);
                    if (director.Photo == null) director.Photo = newPhoto;
                    break;

                case "movie":
                    var movie = await _context.Movies
                        .Include(x => x.Photos)
                        .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Param.Id), cancellationToken);
                    if (movie.Photos.Any()) newPhoto.IsMain = false;
                    movie.Photos.Add(newPhoto);
                    break;
                default:
                    return ResultValidator<Unit>.Error("Pleae Provided Right Parameters", 400);
            }

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ?
            ResultValidator<Unit>.Success(Unit.Value, 200)
            : ResultValidator<Unit>.Error("Error While Saving Photo In Database", 400);
        }
    }
}