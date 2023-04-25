/// <summary>
/// Class <c>List</c> handles the request to list all the movie rooms available.
/// </summary>

using Application.Core;
using Application.MovieRoom.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.MovieRoom;

public class List
{
    /// <summary>
    /// Class <c>Query</c> defines the request to list all the movie rooms available.
    /// </summary>
    public class Query : IRequest<ResultValidator<List<RoomDTO>>> { }
    /// <summary>
    /// Class <c>Handler</c> handles the request to list all the movie rooms available.
    /// </summary>
    public class Handler : IRequestHandler<Query, ResultValidator<List<RoomDTO>>>
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
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the request to list all the movie rooms available.
        /// </summary>
        /// <param name="request">The request to be handled.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A ResultValidator object containing the list of RoomDTO objects and the response code.</returns>
        public async Task<ResultValidator<List<RoomDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Retrieve the list of movie rooms including the attendees
            var roomList = await _context.Room
                .Include(x => x.Attendees)
                .ProjectTo<RoomDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            // Return the list of RoomDTO objects and the response code as a ResultValidator object
            return ResultValidator<List<RoomDTO>>.Success(roomList, 200);
        }
    }
}