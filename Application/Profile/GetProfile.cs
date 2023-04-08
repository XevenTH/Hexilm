using Application.Core;
using Application.Profile.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profile;

public class GetProfile
{
    public class Query : IRequest<ResultValidator<ProfileDTO>> 
    {
        public string Username { get; set; }
    }

    public class Handler : IRequestHandler<Query, ResultValidator<ProfileDTO>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }

        public async Task<ResultValidator<ProfileDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.Photos)
                .ProjectTo<ProfileDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.UserName == request.Username);

            if(user == null) return ResultValidator<ProfileDTO>.Error("Can't Find User", 404);

            return ResultValidator<ProfileDTO>.Success(user, 200);
        }
    }
}
