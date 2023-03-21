using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using MediatR;
using Model;
using Persistence;

namespace Application.Movies;

public class Create
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public MovieDTO MovieDTO { get; set; }
    }

    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var newMovie = _mapper.Map<Movie>(request.MovieDTO);
            
            _context.Movies.Add(newMovie);

            var result = await _context.SaveChangesAsync() > 0;

            if(result != true) return ResultValidator<Unit>.Error("Error Creating The Movie");

            return ResultValidator<Unit>.Success(Unit.Value);
        }
    }
}
