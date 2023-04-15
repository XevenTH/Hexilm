using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies;

public class Single
{
    public class Query : IRequest<ResultValidator<MovieDTO>> 
    { 
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, ResultValidator<MovieDTO>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultValidator<MovieDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .ProjectTo<MovieDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if(movie == null) return ResultValidator<MovieDTO>.Error("Can't Find Movie", 404);

            return ResultValidator<MovieDTO>.Success(movie, 200);
        }
    }
}
