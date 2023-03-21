using Application.Core;
using Application.Interface;
using Application.Profile.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profile;

public class Update
{
    public class Command : IRequest<ResultValidator<Unit>>
    {
        public UpdateProfileDTO RequestProfile { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, ResultValidator<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            _context = context;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }
        
        public async Task<ResultValidator<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

            if (user == null) return ResultValidator<Unit>.Error("Can't Find User");

            _mapper.Map(request.RequestProfile, user);

            var result = await _context.SaveChangesAsync() > 0;

            if (result == false) return ResultValidator<Unit>.Error("Error While Saving Changes");

            return ResultValidator<Unit>.Success(Unit.Value);
        }
    }
}