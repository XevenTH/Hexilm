/// <summary>
/// Handles retrieving a single movie by its Id and mapping it to a DTO.
/// </summary>
using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies;

public class Single
{
    /// <summary>
    /// Query class that contains the Id of the movie to retrieve.
    /// </summary>
    public class Query : IRequest<ResultValidator<MovieDTO>>
    {
        public Guid Id { get; set; }
    }
    /// <summary>
    /// Handler class that retrieves a single movie by its Id and maps it to a DTO.
    /// </summary>
    public class Handler : IRequestHandler<Query, ResultValidator<MovieDTO>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor of the Handler class that initializes the DataContext and IMapper fields.
        /// </summary>
        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Method that retrieves a single movie by its Id and maps it to a DTO.
        /// </summary>
        public async Task<ResultValidator<MovieDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies
                .Include(x => x.Photos)
                .ProjectTo<MovieDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (movie == null) return ResultValidator<MovieDTO>.Error("Can't Find Movie", 404);

            return ResultValidator<MovieDTO>.Success(movie, 200);
        }
    }
}