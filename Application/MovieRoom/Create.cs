using Application.Core;
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
    public class Query : IRequest<ResultValidator<RoomDTO>>
    { 
        public RequestRoomDTO Room { get; set; }
    }

    public class Handler : IRequestHandler<Query, ResultValidator<RoomDTO>>
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

        public async Task<ResultValidator<RoomDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
            if(user == null) return ResultValidator<RoomDTO>.Error("User Unable to Create Room", 404);

            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id.ToString() == request.Room.MovieId);
            if(movie == null) return ResultValidator<RoomDTO>.Error("Movie Not Found", 404);

            Room newRoom = new Room {
                Id = request.Room.Id,
                Title = request.Room.Title,
                Movie = movie
            };

            UserRoom attendee = new UserRoom {
                User = user,
                Room = newRoom,
                IsHost = true,
            };

            newRoom.Attendees.Add(attendee);
            _context.Room.Add(newRoom);

            var result = await _context.SaveChangesAsync() > 0;

            if(result != true) return ResultValidator<RoomDTO>.Error("Error While Creating The Room", 400);

            return ResultValidator<RoomDTO>.Success(_mapper.Map<RoomDTO>(newRoom), 200);
        }
    }
}
