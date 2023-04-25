using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using MediatR;
using Model;
using Persistence;

/// <summary>
/// Handles the creation of a new movie.
/// </summary>
namespace Application.Movies;

public class Create
{
    /// <summary>
    /// Command for creating a new movie.
    /// </summary>
    public class Command : IRequest<ResultValidator<Unit>>
    {
        /// <summary>
        /// Data Transfer Object for the new movie.
        /// </summary>
        public MiniMovieDTO MovieDTO { get; set; }
    }
    /// <summary>
    /// Handler for creating a new movie.
    /// </summary>
    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the Handler.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="mapper">AutoMapper instance</param>
        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the creation of a new movie.
        /// </summary>
        /// <param name="request">Command for creating a new movie</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result validator with success or error message</returns>
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var newMovie = _mapper.Map<Movie>(request.MovieDTO);

            _context.Movies.Add(newMovie);

            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result <= 0) return ResultValidator<Unit>.Error("Error Creating The Movie", 400);

            return ResultValidator<Unit>.Success(Unit.Value, 200);
        }
    }
}