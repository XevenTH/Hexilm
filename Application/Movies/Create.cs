using Application.Core;
using MediatR;
using Model;
using Persistence;

namespace Application.Movies;

public class Create
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public Movie Movie { get; set; }
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
            _context.Movies.Add(request.Movie);

            var result = await _context.SaveChangesAsync() > 0;

            if(result != true) return ResultValidator<Unit>.Error("Error Creating The Movie");

            return ResultValidator<Unit>.Success(Unit.Value);
        }
    }
}
