/// <summary>
/// Class <c>Create</c> handles the creation of a new movie room. It implements the IRequestHandler interface, taking in a Query object with a RequestRoomDTO and returning a ResultValidator with a RoomDTO. It checks if the user exists and if the movie requested exists, creates a new Room and adds the user as the host. If successful, it returns a ResultValidator with the newly created RoomDTO.
/// </summary>
namespace Application.MovieRoom;

using Application.Core;
using Application.Interface;
using Application.MovieRoom.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;
public class Create
{
    /// <summary>
    /// Class <c>Query</c> implements the IRequest interface, taking in a RequestRoomDTO object.
    /// </summary>
    public class Query : IRequest<ResultValidator<RoomDTO>>
    {
        public RequestRoomDTO Room { get; set; }
    }

    /// <summary>
    /// Class <c>Handler</c> handles the logic of creating a new movie room. It implements the IRequestHandler interface, taking in a Query object with a RequestRoomDTO and returning a ResultValidator with a RoomDTO.
    /// </summary>
    public class Handler : IRequestHandler<Query, ResultValidator<RoomDTO>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for Handler class, takes in a DataContext object, an IUserAccessor object, and an IMapper object.
        /// </summary>
        /// <param name="context">A DataContext object for accessing the database</param>
        /// <param name="userAccessor">An IUserAccessor object for accessing the current user</param>
        /// <param name="mapper">An IMapper object for mapping entities to DTOs</param>
        public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Method <c>Handle</c> handles the logic of creating a new movie room. It takes in a Query object with a RequestRoomDTO and returns a ResultValidator with a RoomDTO.
        /// </summary>
        /// <param name="request">A Query object with a RequestRoomDTO</param>
        /// <param name="cancellationToken">A CancellationToken object</param>
        /// <returns>A ResultValidator object with a RoomDTO</returns>
        public async Task<ResultValidator<RoomDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
            if (user == null) return ResultValidator<RoomDTO>.Error("User Unable to Create Room", 404);

            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id.ToString() == request.Room.MovieId);
            if (movie == null) return ResultValidator<RoomDTO>.Error("Movie Not Found", 404);

            Room newRoom = new Room
            {
                Id = request.Room.Id,
                Title = request.Room.Title,
                Movie = movie
            };

            UserRoom attendee = new UserRoom
            {
                User = user,
                Room = newRoom,
                IsHost = true,
            };

            newRoom.Attendees.Add(attendee);
            _context.Room.Add(newRoom);

            var result = await _context.SaveChangesAsync();

            return result > 0 ? ResultValidator<RoomDTO>.Success(_mapper.Map<RoomDTO>(newRoom), 200)
            : ResultValidator<RoomDTO>.Error("Error While Creating The Room", 400);
        }
    }
}
