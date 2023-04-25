using Application.Movies.DTO;
using FluentValidation;

namespace API.Validator;

public class RequestMovieValidator : AbstractValidator<MiniMovieDTO>
{
    public RequestMovieValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }    
}
