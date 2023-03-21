using Application.Core;
using Application.MovieRoom.DTO;
using Application.Movies.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.MovieRoom;

public class List
{
    public class Query : IRequest<ResultValidator<List<RoomDTO>>> { }

    public class Handler : IRequestHandler<Query, ResultValidator<List<RoomDTO>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultValidator<List<RoomDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var roomList = await _context.Room
                .Include(x => x.Attendees)
                .ProjectTo<RoomDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if(roomList == null) return ResultValidator<List<RoomDTO>>.Error("Can't Get List of Room");

            return ResultValidator<List<RoomDTO>>.Success(roomList);
        }
    }
}
