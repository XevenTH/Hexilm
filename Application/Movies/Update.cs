using Application.Core;
using AutoMapper;
using MediatR;
using Model;
using Persistence;

namespace Application.Movies;

public class Update
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public Guid Id { get; set; }
        public Movie Movie { get; set; }
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
            var movie = await _context.Movies.FindAsync(request.Movie.Id);
            if(movie == null) return ResultValidator<Unit>.Error("Can't Find Movie");

            _mapper.Map(request.Movie, movie);

            var result = await _context.SaveChangesAsync() > 0;
            if(result == false) return ResultValidator<Unit>.Error("Error While Updating The Movie");

            return ResultValidator<Unit>.Success(Unit.Value);
        }
    }
}
