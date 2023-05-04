using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies.Details.Director;
public class ListDirector
{
    public class Query : IRequest<ResultValidator<List<DirectorDTO>>> { }
    public class Handler : IRequestHandler<Query, ResultValidator<List<DirectorDTO>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultValidator<List<DirectorDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var director = await _context.Directors
                .Include(x => x.Photo)
                .ProjectTo<DirectorDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return ResultValidator<List<DirectorDTO>>.Success(director, 200);
        }
    }
}
