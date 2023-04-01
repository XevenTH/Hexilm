using Application.Core;
using Application.Interface;
using Application.Movies.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Application.Profile;

public class FavoriteMovieAction
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public FavoriteMovieDTO RequestFavoriteMovie { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }
        
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.FavoriteMovies)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

            if (user == null) return ResultValidator<Unit>.Error("Can't Find The User");

            var movie = await _context.Movies
                .FirstOrDefaultAsync(x => x.Id == request.RequestFavoriteMovie.Id);
            
            if (movie == null) return ResultValidator<Unit>.Error("Can't Find The Movie");

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

            if (result == false) return ResultValidator<Unit>.Error("Error While Saving Changes");

            return ResultValidator<Unit>.Success(Unit.Value);
        }
    }
}