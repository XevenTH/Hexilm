using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

/// <summary>
/// Represents a class that handles the creation of a new actor for a movie in the application.
/// </summary>
namespace Application.Movies.Details.Actor;
/// <summary>
/// Command to create a new actor for a movie.
/// </summary>
public class CreateActor
{
    /// <summary>
    /// Command properties to create a new actor for a movie.
    /// </summary>
    public class Command : IRequest<ResultValidator<ActorDTO>>
    {
        /// <summary>
        /// The identifier of the movie where the actor will be added.
        /// </summary>
        public Guid MovieId { get; set; }
        /// <summary>
        /// The data of the actor to be created.
        /// </summary>
        public ActorDTO actorDTO { get; set; }
    }
    /// <summary>
    /// Handler to create a new actor for a movie.
    /// </summary>
    public class Handler : IRequestHandler<Command, ResultValidator<ActorDTO>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor of the handler.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="mapper">The mapper to map entities to DTOs.</param>
        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Method to handle the creation of a new actor for a movie.
        /// </summary>
        /// <param name="request">The command to create the actor.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A ResultValidator object with the created actor data or an error message.</returns>
        public async Task<ResultValidator<ActorDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            // Find the movie with the given id.
            var movie = await _context.Movies
                .FirstOrDefaultAsync(x => x.Id == request.MovieId, cancellationToken);

            // If the movie is not found, return an error message.
            if (movie == null) return ResultValidator<ActorDTO>.Error("Can't Find The Movie", 404);

            // Create a new actor based on the data provided in the request.
            var newActor = new Model.Actor
            {
                Id = await _context.Actors.CountAsync() + 1,
                Name = request.actorDTO.Name,
            };

            // Add the new actor to the movie's list of actors.
            movie.Actors.Add(newActor);

            // Save the changes to the database.
            var result = await _context.SaveChangesAsync(cancellationToken);

            // If the changes are saved successfully, return the created actor data.
            return result > 0 ?
                ResultValidator<ActorDTO>.Success(_mapper.Map<ActorDTO>(newActor), 200)
                // If the changes cannot be saved, return an error message.
                : ResultValidator<ActorDTO>.Error("Error While Saving Change To Database", 400);
        }
    }
}