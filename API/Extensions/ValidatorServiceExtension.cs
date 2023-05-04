using API.Validator;
using FluentValidation;

namespace API.Extensions;

public static class ValidatorServiceExtension
{
    public static IServiceCollection UseValidatorServiceExtension(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RequestMovieValidator>();
        services.AddValidatorsFromAssemblyContaining<RequestDirectorValidator>();
        services.AddValidatorsFromAssemblyContaining<RequestRoomValidator>();

        return services;
    }
}
