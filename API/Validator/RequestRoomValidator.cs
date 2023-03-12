using Application.MovieRoom.DTO;
using FluentValidation;

namespace API.Validator;

public class RequestRoomValidator : AbstractValidator<RequestRoomDTO>
{
    public RequestRoomValidator()
    {
        RuleFor(x => x.MovieId).NotNull().NotEmpty();
        RuleFor(x => x.Title).NotNull().NotEmpty();
    }
}
