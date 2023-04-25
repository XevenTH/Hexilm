using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

/// <summary>
/// Class <c>CreateDirector</c> handles the creation of a new director for a movie.
/// </summary>
namespace Application.Movies.Details.Director;
/// <summary>
/// Command to create a new director for a movie.
/// </summary>
public class Command : IRequest<ResultValidator<DirectorDTO>>
{
    /// <summary>
    /// Gets or sets the movie id for which the director needs to be added.
    /// </summary>
    public Guid MovieId { get; set; }
    /// <summary>
    /// Gets or sets the director information to be added.
    /// </summary>
    public DirectorDTO actorDTO { get; set; }
}

/// <summary>
/// Handler to create a new director for a movie.
/// </summary>
public class Handler : IRequestHandler<Command, ResultValidator<DirectorDTO>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="Handler"/> class.
    /// </summary>
    /// <param name="context">The data context.</param>
    /// <param name="mapper">The mapper.</param>
    public Handler(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    /// <summary>
    /// Handles the command to create a new director for a movie.
    /// </summary>
    /// <param name="request">The command to create a new director.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A validator result containing the newly created director information if successful.</returns>
    public async Task<ResultValidator<DirectorDTO>> Handle(Command request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies
            .FirstOrDefaultAsync(x => x.Id == request.MovieId, cancellationToken);
        if (movie == null) return ResultValidator<DirectorDTO>.Error("Can't Find The Movie", 404);

        var newDirector = new Model.Director
        {
            Id = await _context.Actors.CountAsync() + 1,
            Name = request.actorDTO.Name,
        };

        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0 ?
        ResultValidator<DirectorDTO>.Success(_mapper.Map<DirectorDTO>(newDirector), 200)
        : ResultValidator<DirectorDTO>.Error("Error While Saving Change To Database", 400);
    }
}