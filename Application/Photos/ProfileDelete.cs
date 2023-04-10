using Application.Core;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos;

public class ProfileDelete
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public string PublicId { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IPhotoAccessor _photoAccessor;

        public Handler(DataContext context, IPhotoAccessor photoAccessor)
        {
            _context = context;
            _photoAccessor = photoAccessor;
        }
        
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var photo = await _context.Photos
                .FirstOrDefaultAsync(x => x.Id == request.PublicId, cancellationToken);
            
            if(photo == null) return ResultValidator<Unit>.Error("Can't Find The Photo", 404);

            if(photo.IsMain) return ResultValidator<Unit>.Error("Can't Delete Main Photo", 404);

            var resultDelete = await _photoAccessor.DeletePhoto(request.PublicId);

            if(resultDelete == null) return ResultValidator<Unit>.Error("Error While Saving Change In Cloud", 400);
            
            _context.Photos.Remove(photo);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            return result
                ? ResultValidator<Unit>.Success(Unit.Value, 200)
                : ResultValidator<Unit>.Error("Error While Saving Change In Database", 400);
        }
    }
}