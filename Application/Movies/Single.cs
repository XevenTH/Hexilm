using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Application.Movies;

public class Single
{
    public class Query : IRequest<Movie> 
    { 
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Movie>
    {
        private readonly DataContext _context;
        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Movie> Handle(Query request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id);

            return movie;
        }
    }
}
