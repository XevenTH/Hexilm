using Application.Movies.DTO;
using FluentValidation;

namespace API.Validator;

public class RequestDirectorValidator : AbstractValidator<DirectorDTO>
{
    public RequestDirectorValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
