using Application.Core;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Application.MovieRoom;

public class UpdateAttendeesAction
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var room = await _context.Room
                .Include(x => x.Attendees)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (room == null) return ResultValidator<Unit>.Error("Can't Find Room", 404);

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken); 

            if (user == null) return ResultValidator<Unit>.Error("Can't Find User", 404);
            
            var userInRoom = room.Attendees.FirstOrDefault(x => x.User.UserName == _userAccessor.GetUsername());

            if (userInRoom == null)
            {
                var newAttendees = new UserRoom
                {
                    User = user,
                    Room = room,
                    IsHost = false
                };
                
                room.Attendees.Add(newAttendees);
            }
            else
            {
                room.Attendees.Remove(userInRoom);
            }

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            
            return result ? ResultValidator<Unit>.Success(Unit.Value, 200) 
                : ResultValidator<Unit>.Error("Something When Wrong....", 400);
        }
    }
}