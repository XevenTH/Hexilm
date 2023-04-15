using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Movies;

public class Update
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public Guid Id { get; set; }
        public MiniMovieDto Movie { get; set; }
    }

    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies.FindAsync(request.Movie.Id, cancellationToken);
            if(movie == null) return ResultValidator<Unit>.Error("Can't Find Movie", 404);

            _mapper.Map(request.Movie, movie);

            var result = await _context.SaveChangesAsync(cancellationToken);
            if(result <= 0) return ResultValidator<Unit>.Error("Error While Updating The Movie", 400);

            return ResultValidator<Unit>.Success(Unit.Value, 200);
        }
    }
}
