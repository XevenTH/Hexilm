using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Application.Movies;

public class Single
{
    public class Query : IRequest<ResultValidator<Movie>> 
    { 
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, ResultValidator<Movie>>
    {
        private readonly DataContext _context;
        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<ResultValidator<Movie>> Handle(Query request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id);

            if(movie == null) return ResultValidator<Movie>.Error("Can't Find Movie");

            return ResultValidator<Movie>.Success(movie);
        }
    }
}
