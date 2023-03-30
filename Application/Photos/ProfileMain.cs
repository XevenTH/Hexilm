using Application.Core;
using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos;

public class ProfileMain
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public string PublicId { get; set; }
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
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken);
            if(user == null) return ResultValidator<Unit>.Error("Can't Find The User");

            var mainPhoto = user.Photos.FirstOrDefault(x => x.IsMain);
            if(mainPhoto == null) return ResultValidator<Unit>.Error("Can't Find The Main Photo");

            mainPhoto.IsMain = false;
            
            var photo = user.Photos.FirstOrDefault(x => x.Id == request.PublicId);
            if(photo == null) return ResultValidator<Unit>.Error("Can't Find The Image");
            
            if (photo.IsMain) return ResultValidator<Unit>.Error("Photo Is Already The Main Photo");

            photo.IsMain = true;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            return result
                ? ResultValidator<Unit>.Success(Unit.Value)
                : ResultValidator<Unit>.Error("Error While Saving Change");
        }
    }
}