using Application.Movies;
using Application.Movies.DTO;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers;

public class MovieController : BaseApiController
{
    private readonly IValidator<MovieDTO> _validator;
    public MovieController(IValidator<MovieDTO> validator)
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
    public async Task<ActionResult<MovieDTO>> CreateMovie([FromBody] MovieDTO requestMovie)
    {
        ValidationResult validateResult = await _validator.ValidateAsync(requestMovie);
        if (!validateResult.IsValid)
        {
            validateResult.AddToModelState(this.ModelState);
            return ValidationProblem();
        }

        var result = await Mediator.Send(new Create.Command { MovieDTO = requestMovie });

        return GetResult(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(Guid id, [FromBody] MovieDTO movie)
    {
        movie.Id = id;
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
