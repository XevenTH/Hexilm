// <summary>
/// Namespace <c>Application.MovieRoom</c> contains classes for managing movie rooms.
/// </summary>
namespace Application.MovieRoom;

using Application.Core;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

/// <summary>
/// Class <c>UpdateAttendeesAction</c> manages the attendees of a movie room.
/// </summary>
public class UpdateAttendeesAction
{
    /// <summary>
    /// Class <c>Command</c> represents the request to update the attendees of a movie room.
    /// </summary>
    public class Command : IRequest<ResultValidator<Unit>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the movie room.
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Class <c>Handler</c> handles the request to update the attendees of a movie room.
    /// </summary>
    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        /// <summary>
        /// Initializes a new instance of the <c>Handler</c> class.
        /// </summary>
        /// <param name="context">The data context for accessing the database.</param>
        /// <param name="userAccessor">The user accessor for accessing the current user.</param>
        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Handles the request to update the attendees of a movie room.
        /// </summary>
        /// <param name="request">The request to update the attendees of a movie room.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>A <c>ResultValidator</c> indicating whether the operation was successful or not.</returns>
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            // Retrieve the movie room from the database.
            var room = await _context.Room
                .Include(x => x.Attendees)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            // Return an error if the movie room cannot be found.
            if (room == null)
            {
                return ResultValidator<Unit>.Error("Can't Find Room", 404);
            }

            // Retrieve the current user from the database.
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken);

            // Return an error if the user cannot be found.
            if (user == null)
            {
                return ResultValidator<Unit>.Error("Can't Find User", 404);
            }

            // Check if the user is already attending the movie room.
            var userInRoom = room.Attendees.FirstOrDefault(x => x.User.UserName == _userAccessor.GetUsername());

            // If the user is not attending the movie room, add them as an attendee.
            if (userInRoom == null)
            {
                var newAttendee = new UserRoom
                {
                    User = user,
                    Room = room,
                    IsHost = false
                };

                room.Attendees.Add(newAttendee);
            }
            // If the user is already attending the movie room, remove them as an attendee.
            else
            {
                room.Attendees.Remove(userInRoom);
            }

            // Save changes to the database.
            var result = await _context.SaveChangesAsync(cancellationToken);

            // If the result is grater than or equal to 0, return an success.
            return result > 0 ? ResultValidator<Unit>.Success(Unit.Value, 200)
            // If the result is less than or equal to 0, return an error.
                : ResultValidator<Unit>.Error("Something When Wrong....", 400);
        }
    }
}