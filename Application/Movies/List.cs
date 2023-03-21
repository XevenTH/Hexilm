using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Application.Movies;

public class List
{
    public class Query : IRequest<ResultValidator<List<MovieDTO>>> { }

    public class Handler : IRequestHandler<Query, ResultValidator<List<MovieDTO>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultValidator<List<MovieDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var movies = await _context.Movies
                .ProjectTo<MovieDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ResultValidator<List<MovieDTO>>.Success(movies);
        }
    }
}
