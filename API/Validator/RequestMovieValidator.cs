using Application.Movies.DTO;
using FluentValidation;
using Model;

namespace API.Validator;

public class RequestMovieValidator : AbstractValidator<MovieDTO>
{
    public RequestMovieValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }    
}
