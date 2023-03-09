using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Application.Movies;

public class List
{
    public class Query : IRequest<ResultValidator<List<Movie>>> { }

    public class Handler : IRequestHandler<Query, ResultValidator<List<Movie>>>
    {
        private readonly DataContext _context;
        public Handler(DataContext context)
        {
            _context = context;

        }

        public async Task<ResultValidator<List<Movie>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var movies = await _context.Movies.ToListAsync();

            if(movies == null) return ResultValidator<List<Movie>>.Error("Movies Not Found");

            return ResultValidator<List<Movie>>.Success(movies);
        }
    }
}
