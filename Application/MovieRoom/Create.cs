using Application.Interface;
using Application.MovieRoom.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Application.MovieRoom;

public class Create
{
    public class Query : IRequest<RoomDTO>
    { 
        public RequestRoomDTO Room { get; set; }
    }

    public class Handler : IRequestHandler<Query, RoomDTO>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<RoomDTO> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

            if(user == null) return null;

            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id.ToString() == request.Room.MovieId);

            if(movie == null) return null;

            Room newRoom = new Room {
                Id = request.Room.Id,
                Title = request.Room.Title,
                Movie = movie
            };

            UserRoom attendee = new UserRoom {
                User = user,
                Room = newRoom
            };

            newRoom.Attendees.Add(attendee);
            _context.Room.Add(newRoom);

            var result = await _context.SaveChangesAsync() > 0;

            if(result == false) return null;

            return _mapper.Map<RoomDTO>(newRoom);
        }
    }
}
