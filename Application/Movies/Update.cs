using MediatR;
using Model;
using Persistence;

namespace Application.Movies;

public class Update
{
    public class Command : IRequest 
    {
        public Guid Id { get; set; }
        public Movie Movie { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies.FindAsync(request.Movie.Id);

            movie.Title = request.Movie.Title;

            var result = await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
