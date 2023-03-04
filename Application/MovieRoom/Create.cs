using Application.Interface;
using Application.MovieRoom.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Application.MovieRoom;

public class Create
{
    public class Command : IRequest 
    { 
        public RoomDTO Room { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id.ToString() == request.Room.MovieId);

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

            var result = await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
