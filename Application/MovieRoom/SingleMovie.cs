/// <summary>
/// Namespace <c>Application.MovieRoom</c> contains classes that handle requests related to movie rooms.
/// </summary>
namespace Application.MovieRoom;

using Application.Core;
using Application.MovieRoom.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
/// <summary>
/// Class <c>SingleMovie</c> handles the request to retrieve a single movie room.
/// </summary>
public class SingleMovie
{
    /// <summary>
    /// Class <c>Query</c> defines the request to retrieve a single movie room.
    /// </summary>
    public class Query : IRequest<ResultValidator<RoomDTO>>
    {
        /// <summary>
        /// The Id of the movie room to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Class <c>Handler</c> handles the request to retrieve a single movie room.
    /// </summary>
    public class Handler : IRequestHandler<Query, ResultValidator<RoomDTO>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <c>Handler</c> class with the specified DataContext and IMapper.
        /// </summary>
        /// <param name="context">The DataContext to be used.</param>
        /// <param name="mapper">The IMapper to be used.</param>
        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Handles the request to retrieve a single movie room.
        /// </summary>
        /// <param name="request">The request to be handled.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A ResultValidator object containing the RoomDTO object and the response code.</returns>
        public async Task<ResultValidator<RoomDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Retrieve the movie room including the attendees
            var room = await _context.Room
                .Include(x => x.Attendees)
                .ProjectTo<RoomDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            // Return an error if the movie room is not found
            if (room == null) return ResultValidator<RoomDTO>.Error("Can't Find Room", 404);

            // Return the RoomDTO object and the response code as a ResultValidator object
            return ResultValidator<RoomDTO>.Success(room, 200);
        }
    }
}