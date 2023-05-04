using Application.Core;
using Application.Movies.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies.Details.Director;
public class UpdateDirector
{
    public class Command : IRequest<ResultValidator<DirectorDTO>>
    {
        public DirectorDTO DirectorDTO { get; set; }
    }

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

        public async Task<ResultValidator<DirectorDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var directorDb = await _context.Directors
                .FirstOrDefaultAsync(d => d.Id == request.DirectorDTO.Id);

            if (directorDb == null)
                return ResultValidator<DirectorDTO>.Error("Can't Find The Director", 404);

            _mapper.Map(request.DirectorDTO, directorDb);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ?
            ResultValidator<DirectorDTO>.Success(_mapper.Map<DirectorDTO>(directorDb), 200)
            : ResultValidator<DirectorDTO>.Error("Error While Updating the Director", 400);
        }
    }
}
