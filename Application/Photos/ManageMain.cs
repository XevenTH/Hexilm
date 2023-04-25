/// <summary>
/// Class <c>ManageMain</c> is a MediatR handler class that manages the user's main photo.
/// </summary>
namespace Application.Photos;

using Application.Core;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class ManageMain
{
    /// <summary>
    /// Class <c>Command</c> is a MediatR command class for managing the user's main photo.
    /// </summary>
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public string PublicId { get; set; }
    }
    /// <summary>
    /// Class <c>Handler</c> is a MediatR handler class that manages the user's main photo.
    /// </summary>
    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        /// <summary>
        /// Initializes a new instance of the <c>Handler</c> class with the specified <c>DataContext</c> and <c>IUserAccessor</c>.
        /// </summary>
        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Handles the <c>Command</c> request to manage the user's main photo.
        /// </summary>
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken);
            if (user == null) return ResultValidator<Unit>.Error("Can't Find The User", 404);

            var mainPhoto = user.Photos.FirstOrDefault(x => x.IsMain);
            if (mainPhoto == null) return ResultValidator<Unit>.Error("Can't Find The Main Photo", 404);

            mainPhoto.IsMain = false;

            var photo = user.Photos.FirstOrDefault(x => x.Id == request.PublicId);
            if (photo == null) return ResultValidator<Unit>.Error("Can't Find The Image", 404);

            if (photo.IsMain) return ResultValidator<Unit>.Error("Photo Is Already The Main Photo", 400);

            photo.IsMain = true;

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0
                ? ResultValidator<Unit>.Success(Unit.Value, 200)
                : ResultValidator<Unit>.Error("Error While Saving Change", 400);
        }
    }
}