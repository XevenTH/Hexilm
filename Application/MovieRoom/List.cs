using Application.MovieRoom.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.MovieRoom;

public class List
{
    public class Query : IRequest<List<RoomDTO>> { }

    public class Handler : IRequestHandler<Query, List<RoomDTO>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RoomDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var roomList = await _context.Room
                .Include(x => x.Attendees)
                .ProjectTo<RoomDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if(roomList == null) return null;

            return roomList;
        }
    }
}
