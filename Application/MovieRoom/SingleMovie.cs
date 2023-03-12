using Application.Core;
using Application.MovieRoom.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.MovieRoom;

public class SingleMovie
{
    public class Query : IRequest<ResultValidator<RoomDTO>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, ResultValidator<RoomDTO>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ResultValidator<RoomDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var room = await _context.Room
                .Include(x => x.Attendees)
                .ProjectTo<RoomDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if(room == null) return ResultValidator<RoomDTO>.Error("Can't Find Room");

            return ResultValidator<RoomDTO>.Success(room);
        }
    }
}
