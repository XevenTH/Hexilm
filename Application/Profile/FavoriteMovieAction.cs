// <summary>
/// Handles adding or removing a movie from user's favorite movies list.
/// </summary>
namespace Application.Profile;

using Application.Core;
using Application.Interface;
using Application.Movies.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

public class FavoriteMovieAction
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        /// <summary>
        /// Gets or sets the favorite movie DTO.
        /// </summary>
        public FavoriteMovieDTO RequestFavoriteMovie { get; set; }
    }

    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="userAccessor">The user accessor service.</param>
        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Handles the request to add or remove a movie from user's favorite movies list.
        /// </summary>
        /// <param name="request">The command containing the favorite movie DTO.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the request with a unit value if successful.</returns>
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.FavoriteMovies)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

            if (user == null) return ResultValidator<Unit>.Error("Can't Find The User", 404);

            var movie = await _context.Movies
                .FirstOrDefaultAsync(x => x.Id == request.RequestFavoriteMovie.Id);

            if (movie == null) return ResultValidator<Unit>.Error("Can't Find The Movie", 404);

            var joinTable = await _context.FavoriteMovies_Join.FindAsync(user.Id, movie.Id);

            if (joinTable == null)
            {
                var newFavMovie = new FavoriteMovies
                {
                    User = user,
                    Movie = movie
                };

                _context.FavoriteMovies_Join.Add(newFavMovie);
            }
            else
            {
                _context.FavoriteMovies_Join.Remove(joinTable);
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (result == false) return ResultValidator<Unit>.Error("Error While Saving Changes", 400);

            return ResultValidator<Unit>.Success(Unit.Value, 200);
        }
    }
}