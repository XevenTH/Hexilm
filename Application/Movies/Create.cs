using MediatR;
using Model;
using Persistence;

namespace Application.Movies;

public class Create
{
    public class Command : IRequest 
    {
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
            _context.Movies.Add(request.Movie);

            var result = await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
