using Application.Movies;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers;

public class MovieController : BaseApiController
{
    private readonly IValidator<Movie> _validator;
    public MovieController(IValidator<Movie> validator)
    {
        _validator = validator;

    }

    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetMovies()
    {
        var result = await Mediator.Send(new List.Query());

        return GetResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovieById(Guid id)
    {
        var result = await Mediator.Send(new Application.Movies.Single.Query { Id = id });

        return GetResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> CreateMovie([FromBody] Movie requestMovie)
    {
        ValidationResult validateResult = await _validator.ValidateAsync(requestMovie);
        if (!validateResult.IsValid)
        {
            validateResult.AddToModelState(this.ModelState);
            return ValidationProblem();
        }

        var result = await Mediator.Send(new Create.Command { Movie = requestMovie });

        return GetResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(Guid id, [FromBody] Movie movie)
    {
        movie.Id = id;
        var result = await Mediator.Send(new Update.Command { Movie = movie });

        return GetResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(Guid id)
    {
        var result = await Mediator.Send(new Delete.Command { Id = id });

        return GetResult(result);
    }
}
