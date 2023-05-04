using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies.Details.Director;
public class DeleteDirector
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<ResultValidator<Unit>> Handle(Command command, CancellationToken cancellationToken)
        {
            var director = await _context.Directors
                .FirstOrDefaultAsync(d => d.Id == command.Id);
            if(director == null)
                return ResultValidator<Unit>.Error("Can't Find The Director", 404);

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Director.Id == command.Id);
            if(movie != null)
                movie.Director = null;
            
            _context.Directors.Remove(director);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ?
            ResultValidator<Unit>.Success(Unit.Value, 200)
            : ResultValidator<Unit>.Error("Error Deleting Director", 400);
        }
    }
}
