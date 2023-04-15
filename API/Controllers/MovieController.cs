using Application.Movies;
using Application.Movies.DTO;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MoviesController : BaseApiController
{
    private readonly IValidator<MiniMovieDto> _validator;
    public MoviesController(IValidator<MiniMovieDto> validator)
    {
        _validator = validator;

    }

    [HttpGet]
    public async Task<ActionResult<List<MovieDTO>>> GetMovies()
    {
        var result = await Mediator.Send(new List.Query());

        return GetResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDTO>> GetMovieById(Guid id)
    {
        var result = await Mediator.Send(new Application.Movies.Single.Query { Id = id });

        return GetResult(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<ActionResult<MovieDTO>> CreateMovie([FromBody] MiniMovieDto miniMovie)
    {
        ValidationResult validateResult = await _validator.ValidateAsync(miniMovie);
        if (!validateResult.IsValid)
        {
            validateResult.AddToModelState(this.ModelState);
            return ValidationProblem();
        }

        var result = await Mediator.Send(new Create.Command { MovieDTO = miniMovie });

        return GetResult(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(Guid id, [FromBody] MiniMovieDto movie)
    {
        var result = await Mediator.Send(new Update.Command { Movie = movie });

        return GetResult(result);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(Guid id)
    {
        var result = await Mediator.Send(new Delete.Command { Id = id });

        return GetResult(result);
    }
}
