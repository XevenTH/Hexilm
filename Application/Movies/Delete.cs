using Application.Core;
using MediatR;
using Model;
using Persistence;

namespace Application.Movies;

public class Delete
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            Movie movie = await _context.Movies.FindAsync(request.Id);

            if(movie == null) return ResultValidator<Unit>.Error("Can't Find Movie", 404);

            _context.Remove(movie);

            var result = await _context.SaveChangesAsync() > 0;

            if(result != true) return ResultValidator<Unit>.Error("Error Deleting The Movie", 400);

            return ResultValidator<Unit>.Success(Unit.Value, 200);
        }
    }
}
