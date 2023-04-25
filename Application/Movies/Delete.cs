using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;


/// <summary>
/// Delete class that contains the Command and Handler to delete a movie from the database.
/// </summary>
namespace Application.Movies;
public class Delete
{
    /// <summary>
    /// Command class that contains the ID of the movie to be deleted.
    /// </summary>
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public Guid Id { get; set; }
    }
    /// <summary>
    /// Handler class that deletes the specified movie from the database.
    /// </summary>
    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;

        /// <summary>
        /// Constructor that initializes the Handler with a DataContext.
        /// </summary>
        public Handler(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that handles the deletion of the specified movie from the database.
        /// </summary>
        /// <param name="request">Command containing the ID of the movie to be deleted.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A ResultValidator of type Unit indicating whether the deletion was successful or not.</returns>
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            // Find the movie to be deleted.
            Movie movie = await _context.Movies
                .Include(x => x.Director)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            // If the movie is not found, return an error.
            if (movie == null) return ResultValidator<Unit>.Error("Can't Find Movie", 404);

            // Remove the movie from the database.
            _context.Remove(movie);

            // Save changes to the database.
            var result = await _context.SaveChangesAsync(cancellationToken);

            // If the result is less than or equal to 0, return an error.
            if (result <= 0) return ResultValidator<Unit>.Error("Error Deleting The Movie", 400);

            // Return a success message.
            return ResultValidator<Unit>.Success(Unit.Value, 200);
        }
    }
}