using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

/// <summary>
/// The List class provides a MediatR Handler to retrieve a list of movies from the database.
/// </summary>
namespace Application.Movies;

public class List
{
    /// <summary>
    /// The Query class inherits from IRequest and specifies the return type of the Handler.
    /// </summary>
    public class Query : IRequest<ResultValidator<List<MovieDTO>>> { }
    /// <summary>
    /// The Handler class inherits from IRequestHandler and is responsible for retrieving a list of movies from the database.
    /// </summary>
    public class Handler : IRequestHandler<Query, ResultValidator<List<MovieDTO>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructs a new instance of the Handler class.
        /// </summary>
        /// <param name="context">The DataContext instance used to communicate with the database.</param>
        /// <param name="mapper">The IMapper instance used to map entities to DTOs.</param>
        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of movies from the database and maps them to DTOs.
        /// </summary>
        /// <param name="request">The Query instance representing the request.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the request.</param>
        /// <returns>A ResultValidator instance containing the list of movie DTOs.</returns>
        public async Task<ResultValidator<List<MovieDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var movies = await _context.Movies
                .Include(x => x.Photos)
                .ProjectTo<MovieDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return ResultValidator<List<MovieDTO>>.Success(movies, 200);
        }
    }
}