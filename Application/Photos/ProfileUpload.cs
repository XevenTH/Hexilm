using Application.Core;
using Application.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model;
using Persistence;

namespace Application.Photos;

public class ProfileUpload
{
    public class Command : IRequest<ResultValidator<Photo>>
    {
        public IFormFile File { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, ResultValidator<Photo>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IPhotoAccessor _photoAccessor;

        public Handler(DataContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
            _photoAccessor = photoAccessor;
        }
        
        public async Task<ResultValidator<Photo>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.Photo)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken);

            if (user == null) return ResultValidator<Photo>.Error("Can't Find User");

            var uploadResult = await _photoAccessor.UploadPhoto(request.File);
            
            if(uploadResult == null) return ResultValidator<Photo>.Error("Please Input The Photo File");

            var photo = new Photo()
            {
                Id = uploadResult.PublicId,
                Url = uploadResult.Url,
            };

            if (!user.Photo.Any(x => x.IsMain)) photo.IsMain = true;

            user.Photo.Add(photo);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            return result 
                ? ResultValidator<Photo>.Success(photo) 
                : ResultValidator<Photo>.Error("There Is Problem While Saving Photo");
        }
    }
}