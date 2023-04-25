/// <summary>
/// The Update class is responsible for handling the update of a Movie object.
/// </summary>
using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies;

public class Update
{
    /// <summary>
    /// Command class that implements IRequest for the ResultValidator of Unit.
    /// </summary>
    public class Command : IRequest<ResultValidator<Unit>>
    {
        /// <summary>
        /// The unique identifier of the Movie object.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The MovieDTO object containing the updated information of the Movie.
        /// </summary>
        public RequestUpdateDTO Movie { get; set; }
    }

    /// <summary>
    /// Handler class that implements IRequestHandler for the Command and ResultValidator of Unit.
    /// </summary>
    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor of the Handler class that receives a DataContext and IMapper object.
        /// </summary>
        /// <param name="context">The DataContext object used to connect to the database.</param>
        /// <param name="mapper">The IMapper object used to map objects.</param>
        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// The Handle method that receives a Command and a CancellationToken objects and returns a ResultValidator of Unit.
        /// </summary>
        /// <param name="request">The Command object containing the information of the update.</param>
        /// <param name="cancellationToken">The CancellationToken object used to cancel the request.</param>
        /// <returns>A ResultValidator of Unit.</returns>
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (movie == null) return ResultValidator<Unit>.Error("Can't Find Movie", 404);

            _mapper.Map(request.Movie, movie);

            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result <= 0) return ResultValidator<Unit>.Error("Error While Updating The Movie", 400);

            return ResultValidator<Unit>.Success(Unit.Value, 200);
        }
    }
}
