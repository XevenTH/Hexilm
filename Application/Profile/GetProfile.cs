/// <summary>
/// Class <c>GetProfile</c> is a MediatR handler that retrieves a user's profile based on their username.
/// </summary>
namespace Application.Profile;

using Application.Core;
using Application.Profile.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class GetProfile
{
    /// <summary>
    /// Class <c>Query</c> is a request object for <c>GetProfile</c> MediatR handler.
    /// </summary>
    public class Query : IRequest<ResultValidator<ProfileDTO>>
    {
        public string Username { get; set; }
    }

    /// <summary>
    /// Class <c>Handler</c> is a MediatR handler that retrieves a user's profile based on their username.
    /// </summary>
    public class Handler : IRequestHandler<Query, ResultValidator<ProfileDTO>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <c>Handler</c> class.
        /// </summary>
        /// <param name="context">An instance of the <c>DataContext</c> class.</param>
        /// <param name="mapper">An instance of the <c>IMapper</c> interface.</param>
        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Retrieves a user's profile based on their username.
        /// </summary>
        /// <param name="request">A <c>Query</c> object that contains the user's username.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A <c>ResultValidator</c> object that contains a <c>ProfileDTO</c> object.</returns>
        public async Task<ResultValidator<ProfileDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.Photos)
                .ProjectTo<ProfileDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.UserName == request.Username);

            if (user == null) return ResultValidator<ProfileDTO>.Error("Can't Find User", 404);

            return ResultValidator<ProfileDTO>.Success(user, 200);
        }
    }
}