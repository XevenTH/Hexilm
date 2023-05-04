using Application.Movies.Details.Director;
using Application.Movies.DTO;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DirectorController : BaseApiController
{
    private readonly IValidator<DirectorDTO> _validator;

    public DirectorController(IValidator<DirectorDTO> validator)
    {
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<List<DirectorDTO>>> GetDirectors()
    {
        var result = await Mediator.Send(new ListDirector.Query());

        return GetResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteDirector.Command { Id = id });

        return GetResult(result);
    }

    [HttpPost("{movieId}")]
    public async Task<ActionResult<DirectorDTO>> Create(Guid movieId, [FromBody] DirectorDTO director)
    {
        ValidationResult validateResult = await _validator.ValidateAsync(director);
        if(!validateResult.IsValid)
        {
            validateResult.AddToModelState(this.ModelState);
            return ValidationProblem();
        }

        var result = await Mediator.Send(new CreateDirector.Command
        {
            DirectorDTO = director,
            MovieId = movieId
        });

        return GetResult(result);
    }

    [HttpPut]
    public async Task<ActionResult<DirectorDTO>> Update([FromBody] DirectorDTO director)
    {
        ValidationResult validateResult = await _validator.ValidateAsync(director);
        if(!validateResult.IsValid)
        {
            validateResult.AddToModelState(this.ModelState);
            return ValidationProblem();
        }

        var result = await Mediator.Send(new UpdateDirector.Command
        {
            DirectorDTO = director,
        });

        return GetResult(result);
    }
}
