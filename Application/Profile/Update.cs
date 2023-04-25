/// <summary>
/// Class <c>Update</c> handles updating user profile information.
/// </summary>
namespace Application.Profile;

using Application.Core;
using Application.Interface;
using Application.Profile.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
/// <summary>
/// Command class for updating user profile.
/// </summary>
public class Update
{
    /// <summary>
    /// Command object for updating user profile.
    /// </summary>
    public class Command : IRequest<ResultValidator<Unit>>
    {
        /// <summary>
        /// DTO object for the updated user profile.
        /// </summary>
        public UpdateProfileDTO RequestProfile { get; set; }
    }

    /// <summary>
    /// Handler class for updating user profile.
    /// </summary>
    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the Handler class.
        /// </summary>
        /// <param name="context">The data context for the application.</param>
        /// <param name="userAccessor">The user accessor for the application.</param>
        /// <param name="mapper">The mapper for the application.</param>
        public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            _context = context;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for handling the update user profile command.
        /// </summary>
        /// <param name="request">The update user profile command object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A ResultValidator containing the status of the update.</returns>
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            // Find the user to be updated
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == _userAccessor.GetId());

            // Return error if user not found
            if (user == null) return ResultValidator<Unit>.Error("Can't Find User", 404);

            // Map updated user profile to user object
            _mapper.Map(request.RequestProfile, user);

            // If username has been updated, update NormalizedUserName
            if (request.RequestProfile.UserName.ToUpper() != user.NormalizedUserName) user.NormalizedUserName = request.RequestProfile.UserName.ToUpper();

            // Save changes to the database
            var result = await _context.SaveChangesAsync() > 0;

            // Return error if save changes fails
            if (result == false) return ResultValidator<Unit>.Error("Error While Saving Changes", 400);

            // Return success if update is successful
            return ResultValidator<Unit>.Success(Unit.Value, 200);
        }
    }
}