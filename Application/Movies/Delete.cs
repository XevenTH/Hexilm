using MediatR;
using Model;
using Persistence;

namespace Application.Movies;

public class Delete
{
    public class Command : IRequest
    {
        public Guid Id { get; set; }
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
            Movie movie = await _context.Movies.FindAsync(request.Id);

            _context.Remove(movie);

            var result = await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
